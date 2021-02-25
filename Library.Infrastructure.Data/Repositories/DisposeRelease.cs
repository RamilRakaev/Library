using System;
using System.Collections.Generic;
using System.Text;


namespace Library.Infrastructure.Bll.Repositories
{
    public class DisposeRelease:IDisposable
    {
        protected LibraryContext db;

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
