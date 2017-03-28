using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BlueSignalCommon;
using System.Data.Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace BlueSignalCore.Context
{
    /// <summary>
    /// The supported operations in where-extension
    /// </summary>
    public enum WhereOperation
    {
        Equal,
        NotEqual,
        Contains
    }
    public static class ExtensionMethods
    {

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool isAscending)
        {
            var type = typeof(T);
            var methodName = isAscending ? "OrderBy" : "OrderByDescending";
            var property = (type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ??
                            type.GetProperty(propertyName + Constants.IdColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)) ??
                           type.GetProperty(Constants.IdColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            //Property Sorting for the external Table Columns, Below is the temprory Fix
            if (property != null)
            {
                if (property.PropertyType.IsClass)
                {
                    if (property.PropertyType.FullName != "System.String")
                    {
                        property = type.GetProperty(Constants.IdColumn,
                                                    BindingFlags.IgnoreCase | BindingFlags.Public |
                                                    BindingFlags.Instance);
                    }
                }
            }
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), methodName, new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query,
           string column, object value)
        {
            return query.Where(column, value, WhereOperation.Contains);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query,
            string column, object value, WhereOperation operation)
        {
            if (string.IsNullOrEmpty(column)) { return query; }

            var parameter = Expression.Parameter(query.ElementType, "p");

            var memberAccess = column.Split('.').Aggregate<string, MemberExpression>(null, (current, property) => Expression.Property(current ?? (parameter as Expression), property));

            //change param value type necessary to getting bool from string
            ConstantExpression filter = null;
            try
            {
                filter = Expression.Constant
                (
                    Convert.ChangeType(value, memberAccess.Type,CultureInfo.InvariantCulture)
                 );
            }
            catch
            {
                return query;
            }

            //switch operation
            LambdaExpression lambda = null;
            try
            {
                Expression condition = null;
                switch (operation)
                {
                    case WhereOperation.Equal:
                        condition = Expression.Equal(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    case WhereOperation.NotEqual:
                        condition = Expression.NotEqual(memberAccess, filter);
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                    case WhereOperation.Contains:
                        condition = Expression.Call(memberAccess,
                            typeof(string).GetMethod("Contains"),
                            Expression.Constant(value));
                        lambda = Expression.Lambda(condition, parameter);
                        break;
                }
            }
            catch
            {
                return query;
            }

            var result = Expression.Call(
                   typeof(Queryable), "Where",
                   new[] { query.ElementType },
                   query.Expression,
                   lambda);

            return query.Provider.CreateQuery<T>(result);
        }

        /// <summary>
        /// Searches in all string properties for the specifed search key.  
        /// It is also able to search for several words. If the searchKey is for example 'Chandru BK' then  
        /// all records which contain either 'Chandru' or 'BK' in some string property are returned.   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> query, string value)
        {
            return FullTextSearch<T>(query, value, false);
        }

          
         /// <summary>  
         /// Searches in all string properties for the specifed search key.  
         /// It is also able to search for several words. If the searchKey is for example 'John Travolta' then  
         /// with exactMatch set to false all records which contain either 'John' or 'Travolta' in some string property  
         /// are returned.  
         /// </summary>  
         /// <typeparam name="T"></typeparam>  
        /// <param name="queryable"></param>  
         /// <param name="searchKey"></param>  
         /// <param name="exactMatch">Specifies if only the whole word or every single word should be searched.</param>  
         /// <returns></returns>
        public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> queryable, string searchKey, bool exactMatch)
        {
            ParameterExpression parameter = Expression.Parameter(typeof (T), "c");
            MethodInfo containsMethod = typeof (string).GetMethod("Contains", new Type[] {typeof (string)});
            //MethodInfo toStringMethod = typeof (object).GetMethod("ToString", new Type[] {});
            var publicProperties =
                typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(
                    p => p.PropertyType == typeof (string));
            Expression orExpressions = null;
            string[] searchKeyParts;
            if (exactMatch)
            {
                searchKeyParts = new[] {searchKey};
            }
            else
            {
                searchKeyParts = searchKey.Split(' ');
            }
            foreach (var property in publicProperties)
            {
                Expression nameProperty = Expression.Property(parameter, property);
                foreach (var searchKeyPart in searchKeyParts)
                {
                    Expression searchKeyExpression = Expression.Constant(searchKeyPart);
                    Expression callContainsMethod = Expression.Call(nameProperty, containsMethod, searchKeyExpression);
                    if (orExpressions == null)
                    {
                        orExpressions = callContainsMethod;
                    }
                    else
                    {
                        orExpressions = Expression.Or(orExpressions, callContainsMethod);
                    }
                }
            }
            MethodCallExpression whereCallExpression = Expression.Call(typeof (Queryable), "Where",
                                                                       new Type[] {queryable.ElementType},
                                                                       queryable.Expression,
                                                                       Expression.Lambda<Func<T, bool>>(orExpressions,
                                                                                                        new ParameterExpression
                                                                                                            []
                                                                                                            {parameter}));
            return queryable.Provider.CreateQuery<T>(whereCallExpression);
        }


        public static MultiResultSetReader MultiResultSetSqlQuery(this DbContext context, string query, params SqlParameter[] parameters)
        {
            return new MultiResultSetReader(context, query, parameters);
        }
    }

    public class MultiResultSetReader : IDisposable
    {
        private readonly DbContext _context;
        private readonly DbCommand _command;
        private bool _connectionNeedsToBeClosed;
        private DbDataReader _reader;

        public MultiResultSetReader(DbContext context, string query, SqlParameter[] parameters)
        {
            _context = context;
            _command = _context.Database.Connection.CreateCommand();
            _command.CommandText = query;

            if (parameters != null && parameters.Any()) _command.Parameters.AddRange(parameters);
        }


        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            if (_connectionNeedsToBeClosed)
            {
                _context.Database.Connection.Close();
                _connectionNeedsToBeClosed = false;
            }
        }

        public ObjectResult<T> ResultSetFor<T>()
        {
            if (_reader == null)
            {
                _reader = GetReader();
            }
            else
            {
                _reader.NextResult();
            }

            var objContext = ((IObjectContextAdapter)_context).ObjectContext;

            return objContext.Translate<T>(_reader);
        }

        private DbDataReader GetReader()
        {
            if (_context.Database.Connection.State != ConnectionState.Open)
            {
                _context.Database.Connection.Open();
                _connectionNeedsToBeClosed = true;
            }

            return _command.ExecuteReader();
        }
    }

    
}
