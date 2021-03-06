using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SharpBits.Base
{
    public class BitsFiles: List<BitsFile>, IDisposable
    {
        private IEnumBackgroundCopyFiles fileList;
        private BitsJob job;
        private bool disposed;

        internal BitsFiles(BitsJob job, IEnumBackgroundCopyFiles fileList)
        {
            this.fileList = fileList;
            this.job = job;
            this.Refresh();
        }

        internal void Refresh()
        {
            uint count;
            IBackgroundCopyFile currentFile;
            uint fetchedCount = 0;
            this.fileList.Reset();
            this.Clear();
            this.fileList.GetCount(out count);
            for (int i = 0; i < count; i++)
            {
                this.fileList.Next(1, out currentFile, out fetchedCount);
                if (fetchedCount == 1)
                {
                    this.Add(new BitsFile(this.job, currentFile));
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //TODO: release COM resource
                    this.fileList = null;
                }
            }
            disposed = true;
        }


        #endregion
    }
}
