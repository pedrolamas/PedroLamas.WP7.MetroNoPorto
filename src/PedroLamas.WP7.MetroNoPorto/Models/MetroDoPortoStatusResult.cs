using System;

namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public class MetroDoPortoStatusResult : IMetroDoPortoStatusResult
    {
        #region Properties

        public Exception Error { get; protected set; }

        public IMetroDoPortoLine[] Lines { get; protected set; }

        public DateTimeOffset TimeStamp { get; protected set; }

        #endregion

        public MetroDoPortoStatusResult(Exception error)
        {
            Error = error;
        }

        public MetroDoPortoStatusResult(IMetroDoPortoLine[] lines)
        {
            Lines = lines;
            TimeStamp = DateTimeOffset.Now;
        }
    }
}