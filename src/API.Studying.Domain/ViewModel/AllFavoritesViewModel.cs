using System.Collections.Generic;

namespace API.Studying.Domain.ViewModel
{
    public class AllFavoritesViewModel
    {
        public List<DocumentViewModel> DocumentsFavorites { get; set; }
        public List<VideoViewModel> VideosFavorites { get; set; }
        public List<TopicViewModel> TopicsFavorites { get; set; }

        public AllFavoritesViewModel()
        {
            DocumentsFavorites = new List<DocumentViewModel>();
            VideosFavorites = new List<VideoViewModel>();
            TopicsFavorites = new List<TopicViewModel>();
        }
    }
}
