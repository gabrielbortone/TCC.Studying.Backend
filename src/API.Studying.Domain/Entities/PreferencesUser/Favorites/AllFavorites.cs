using System.Collections.Generic;

namespace API.Studying.Domain.Entities.PreferencesUser.Favorites
{
    public class AllFavorites
    {
        public List<Document> Documents { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Video> Videos { get; set; }
        public AllFavorites()
        {
            Documents = new List<Document>();
            Topics = new List<Topic>();
            Videos = new List<Video>();
        }
    }
}
