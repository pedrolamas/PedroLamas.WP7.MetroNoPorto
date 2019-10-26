using System;

namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public interface IMetroDoPortoService
    {
        void GetStatus(Action<IMetroDoPortoStatusResult> callback);
    }
}