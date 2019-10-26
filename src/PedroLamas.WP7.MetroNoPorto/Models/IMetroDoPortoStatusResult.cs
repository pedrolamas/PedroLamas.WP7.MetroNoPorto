using System;

namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public interface IMetroDoPortoStatusResult
    {
        IMetroDoPortoLine[] Lines { get; }

        DateTimeOffset TimeStamp { get; }

        Exception Error { get; }
    }
}