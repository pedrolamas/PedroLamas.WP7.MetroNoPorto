namespace PedroLamas.WP7.MetroNoPorto.Models
{
    public class MetroDoPortoLine : IMetroDoPortoLine
    {
        #region Properties

        public int Id { get; protected set; }

        public string Description { get; protected set; }

        public string Color { get; protected set; }

        public string StatusTitle { get; protected set; }

        public string StatusDescription { get; protected set; }

        #endregion

        public MetroDoPortoLine(int id, string description, string statusTitle, string statusDescription)
        {
            Id = id;
            Description = description;
            StatusTitle = statusTitle;
            StatusDescription = statusDescription;
        }
    }
}