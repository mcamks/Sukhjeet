using BlueSignalCore.Repository;
using BlueSignalCore.UOW;
using System;

namespace BlueSignalCore.Bal
{
    public class BaseBal : IDisposable
    {
        private bool _disposed;
        public readonly UnitOfWork uw = new UnitOfWork();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                uw.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
