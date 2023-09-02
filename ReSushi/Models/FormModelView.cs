namespace ReSushi.Models
{
    public class FormCategoryView
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class FormProductView
    {
        public string Name { get; set; }
        public string Sostav { get; set; }
        public string Detail { get; set; }
        public int idCategory { get; set; }
    }
}
