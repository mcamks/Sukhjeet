// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Spadez">
//   OmniHealthcare
// </copyright>
// <summary>
//   The unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace BlueSignalCore.UOW
{
    using Models;
    using Repository;
    using System;

    /// <summary>
    ///     The unit of work.
    /// </summary>
    public class UnitOfWork
    {
        #region Fields

        /// <summary>
        ///     The _context.
        /// </summary>
        private readonly BlueSignalContext _context = new BlueSignalContext();

        /// <summary>
        ///     The _disposed.
        /// </summary>
        private bool _disposed;
        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }

            this._disposed = true;
        }

        #endregion

        #region Repositories
        private MarketDataRepository _MarketDataRepository;
        public MarketDataRepository MarketDataRepository
        {
            get
            {
                if (_MarketDataRepository == null)
                    _MarketDataRepository = new MarketDataRepository();
                return _MarketDataRepository;
            }
        }


        private MarketCategoryRepository _MarketCategoryRepository;
        public MarketCategoryRepository MarketCategoryRepository
        {
            get
            {
                if (_MarketCategoryRepository == null)
                    _MarketCategoryRepository = new MarketCategoryRepository();
                return _MarketCategoryRepository;
            }
        }


        #endregion
    }
}
