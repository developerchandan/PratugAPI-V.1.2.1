using Prayug.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Prayug.Infrastructure.ResponseFormat
{
    public class SingleListResponse<TModel> : ISingleListResponse<TModel>, IDisposable
    {
        public long timestamp { get; set; } = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssmm"));
        //private long timestamp_;
        //public long timestamp
        //{
        //    get { return timestamp_; }
        //    set
        //    {
        //        if (value > 0)
        //        {
        //            timestamp_ = value;
        //        }
        //        else
        //        {
        //            timestamp_ = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssmm"));
        //        }

        //    }
        //}
        [JsonIgnore]
        public Int32 pageSize { get; set; }
        public TModel objResponse { get; set; }
        public Int64 TotalRecord { get; set; }
        public int NumberOfPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecord / ((pageSize > 0) ? pageSize : 10));
            }
        }
        public string Message { get; set; }
        public ResponseMessageEnum Status { get; set; }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SingleListResponse()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
