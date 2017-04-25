using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using BlueSignalCore.Dto;
using BlueSignalCore.Models;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using BlueSignalCommon;
using BlueSignalCore.Repository;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;

namespace BlueSignalCore.Bal
{
    public class MarketBal : BaseBal
    {
        private readonly IRepository<Users> _usersRep;
        private readonly IRepository<EmailTemplate> _emailTemplateRep;
        private readonly IRepository<ContactLog> _contactLogRep;
        private readonly IRepository<MarketData> _marketDataRep;
        private readonly IRepository<MarketCategory> _marketCategoryRep;


        public MarketBal(IRepository<Users> usersRep, IRepository<EmailTemplate> emailTemplateRep, IRepository<ContactLog> contactLogRep, IRepository<MarketCategory> marketCategoryRep, IRepository<MarketData> marketDataRep)
        {
            _usersRep = usersRep;
            _emailTemplateRep = emailTemplateRep;
            _contactLogRep = contactLogRep;
            _marketCategoryRep = marketCategoryRep;
            _marketDataRep = marketDataRep;


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MarketData, MarketDataDto>();
                cfg.CreateMap<MarketDataDto, MarketData>();
            });
        }

        public async Task<IEnumerable<MarketDataDto>> GetMarketData()
        {
            try
            {
                var list = new List<MarketDataDto>();
                var m = await _marketDataRep.Where(a => a.IsActive).ToListAsync();
                if (m.Any())
                {
                    list.AddRange(m.Select(a =>
                        {
                            var vm = Mapper.Map<MarketDataDto>(a);
                            if (vm != null && vm.CategoryId.HasValue)
                                vm.Category = _marketCategoryRep.Where(w => w.Id == vm.CategoryId.Value).Select(c => c.CategoryName).FirstOrDefault();
                            return vm;
                        }));
                }
                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveMarketData(MarketDataDto vm)
        {
            var result = -1;
            try
            {
                vm.IsActive = true;
                TimeSpan? cTime = null;
                if (!string.IsNullOrEmpty(vm.Time))
                    cTime = TimeSpan.Parse(vm.Time);

                using (var rep = uw.MarketDataRepository)
                {
                    var model = Mapper.Map<MarketData>(vm);

                    if (model.Id > 0)
                    {
                        var current = rep.GetSingle(model.Id);
                        current.ModifiedBy = 1;
                        current.ModifiedDate = DateTime.Now;
                        current.EntryDate = vm.EntryDate.AddTimeToDateValue(cTime);
                        current.EntryPrice = vm.EntryPrice;
                        current.ExitDate = vm.ExitDate;
                        current.IsActive = vm.IsActive;
                        current.ModifiedBy = vm.ModifiedBy;
                        current.ModifiedDate = vm.ModifiedDate;
                        current.Price = vm.Price;
                        current.ProductTypeID = vm.ProductTypeID;
                        current.QuantTradingType = vm.QuantTradingType;
                        current.Result = vm.Result;
                        current.SymbolCode = vm.SymbolCode;
                        current.SymbolName = vm.SymbolName;
                        current.CategoryId = vm.CategoryId;

                        result = Convert.ToInt32(rep.UpdateEntity(current, current.Id));
                    }
                    else
                    {
                        model.CreatedBy = 1;
                        model.CreatedDate = DateTime.Now;
                        if (vm.EntryDate.HasValue)
                            model.EntryDate = vm.EntryDate.AddTimeToDateValue(cTime);
                        result = Convert.ToInt32(rep.Create(model));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<MarketDataDto>> GetMarketData(string productTypeId)
        {
            try
            {
                var list = new List<MarketDataDto>();
                using (var rep = uw.MarketDataRepository)
                {
                    //var m = (productTypeId.Equals("101") || productTypeId.Equals("102")) ? rep.Where(a => a.IsActive && (a.ProductTypeID.Equals("101") || a.ProductTypeID.Equals("102"))).ToList() : rep.Where(a => a.IsActive && a.ProductTypeID.Equals(productTypeId)).ToList();
                    var m = rep.Where(a => a.IsActive && a.ProductTypeID.Equals(productTypeId)).ToList();
                    if (m.Any())
                    {
                        list.AddRange(m.Select(a =>
                        {
                            var vm = Mapper.Map<MarketDataDto>(a);
                            if (vm != null && vm.CategoryId.HasValue)
                            {
                                using (var rep1 = uw.MarketCategoryRepository)
                                    vm.Category = rep1.Where(w => w.Id == vm.CategoryId.Value).Select(c => c.CategoryName).FirstOrDefault();
                            }
                            return vm;
                        }));
                    }
                }
                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MarketDataDto> GetMarketDataById(long id)
        {
            try
            {
                var vm = new MarketDataDto();
                using (var rep = uw.MarketDataRepository)
                {
                    var m = rep.Where(a => a.Id == id).FirstOrDefault();
                    if (m != null)
                        vm = Mapper.Map<MarketDataDto>(m);
                }
                return await Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WP_User GetWpUser(string un, string pwd)
        {
            var user = new WP_User();

            MySqlConnection myConnection = new MySqlConnection(ConfigurationSettings.AppSettings["Wb_ConnectionString"]);
            //string strSQL = "SELECT * FROM wp_g3b4k2u7_users where user_login='" + un + "' and user_pass='" + pwd + "'";
            string strSQL = "SELECT * FROM wp_g3b4k2u7_users where user_login='" + un + "'";
            MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(strSQL, myConnection);
            DataSet myDataSet = new DataSet();
            myDataAdapter.Fill(myDataSet, "my_users");


            if (myDataSet != null && myDataSet.Tables.Count > 0 && myDataSet.Tables[0].Rows.Count > 0)
            {
                var objInList = myDataSet.Tables[0].Rows[0];
                user.ID = Convert.ToString(objInList["ID"]).Trim();
                user.user_login = Convert.ToString(objInList["user_login"]).Trim();
                user.user_password = Convert.ToString(objInList["user_pass"]).Trim();
                user.user_nicename = Convert.ToString(objInList["user_nicename"]).Trim();
                user.user_email = Convert.ToString(objInList["user_email"]).Trim();
                user.user_registered = Convert.ToString(objInList["user_registered"]).Trim();
                user.display_name = Convert.ToString(objInList["display_name"]).Trim();
            }

            DataSet myDataSetUserType = new DataSet();
            if (Convert.ToString(user.ID).Length > 0)
            {
                MySqlConnection myConnectionUserType = new MySqlConnection(ConfigurationSettings.AppSettings["Wb_ConnectionString"]);
                string strSQLUserType = "select * from  wp_g3b4k2u7_usermeta where user_id = '" + Convert.ToString(user.ID) + "' and meta_key = 'wp_g3b4k2u7_capabilities' ";
                MySqlDataAdapter myDataAdapterUserType = new MySqlDataAdapter(strSQLUserType, myConnectionUserType);

                myDataAdapterUserType.Fill(myDataSetUserType, "my_usersUserType");

            }
            if (myDataSetUserType != null && myDataSetUserType.Tables.Count > 0 && myDataSetUserType.Tables[0].Rows.Count > 0)
            {

                var objInListUserType = myDataSetUserType.Tables[0].Rows[0];
                user.display_AdminKey = Convert.ToString(objInListUserType["meta_value"]).Trim();
            }

            if (user != null && user.ID != "0" && user.ID != "")
            {
                return user;
            }

            return new WP_User();
        }

        public async Task<int> DeleteMarketData(long id)
        {
            var result = -1;
            try
            {
                using (var rep = uw.MarketDataRepository)
                {
                    var current = rep.GetSingle(id);
                    current.IsActive = false;
                    rep.UpdateEntity(current, current.Id);
                    result = Convert.ToInt32(current.ProductTypeID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<SelectItem>> GetActiveMarketCategories()
        {
            var list = new List<SelectItem>();
            using (var rep = uw.MarketCategoryRepository)
            {
                var categories = await rep.Where(a => a.IsActive != null && a.IsActive.Value).ToListAsync();
                if (categories.Any())
                {
                    list.AddRange(categories.Select(a => new SelectItem
                    {
                        Text = a.CategoryName,
                        Value = Convert.ToString(a.Id),
                        SortOrder = a.SortOrder
                    }).OrderBy(a => a.SortOrder));
                }
                return list;
            }
        }

        #region Users Section
        public async Task<Users> IsUserExists(string userName, string pwd = "")
        {
            try
            {
                //To pass through the authentication if user comes from wordpress site...This case needs to be handled later.
                if (!string.IsNullOrEmpty(userName))
                    pwd = string.Empty;

                var result = _usersRep.Where(x => x.Email.Equals(userName) && (string.IsNullOrEmpty(pwd) || x.PasswordHash.Equals(pwd)));
                return await result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<Users> AuthenticateUser(string userName, string pwd)
        {
            try
            {
                var result = _usersRep.Where(x => x.Email.Equals(userName) && x.PasswordHash.Equals(pwd));
                return await result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<bool> CheckUserAndUpdatePassword(string userName, string email, string pwd)
        {
            bool result;
            var user = await _usersRep.Where(x => (x.Email.Equals(email) || x.UserName.ToLower().Equals(userName))).FirstOrDefaultAsync();

            if (user != null)
            {
                user.IsActive = true;
                user.PasswordHash = pwd;
                _usersRep.Update(user);
                result = true;
            }
            else
            {
                user = new Users
                {
                    IsActive = true,
                    PasswordHash = pwd,
                    UserName = userName,
                    Email = email,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                _usersRep.Insert(user);
                result = true;
            }
            return result;
        }
        public void SaveUser(Users model)
        {
            _usersRep.Insert(model);
        }

        #endregion

        #region Email Template and Contact Log
        public async Task<EmailTemplate> SaveContactLog(ContactLog model)
        {
            _contactLogRep.Insert(model);

            var emailTemp = await _emailTemplateRep.Where(e => e.EmailType == 1).FirstOrDefaultAsync();
            return emailTemp;
        }
        #endregion
    }

    public static class Extensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(System.DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                    property.SetValue(item, day, null);
                }
                else
                {
                    property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }
    }
}
