namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public interface IMetroDoPortoLine
    {
        int Id { get; }

        string Description { get; }

        string Color { get; }

        string StatusTitle { get; }

        string StatusDescription { get; }
    }
}