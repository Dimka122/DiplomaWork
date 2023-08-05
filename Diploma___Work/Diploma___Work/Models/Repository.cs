namespace Diploma___Work.Models
{
    public class Repository
    {
        private ApplicationDbContext context=new ApplicationDbContext();

        public IEnumerable<Sushi> Sushis
        {
            get { return context.Sushi; }
        }
    }
}
