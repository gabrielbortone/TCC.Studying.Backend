using System;
using System.Collections.Generic;

namespace API.Studying.Domain.ViewModel
{
    public class StudentCompleteInfoViewModel
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string ImageUrl { get; private set; }
        public string Telephone { get; private set; }
        public int Points { get; private set; }
        public string Scholarity { get; private set; }
        public string Institution { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsBlocked { get; private set; }

        public List<DocumentViewModelWithoutAuthor> DocumentsPosted { get; set; }
        public List<VideoViewModelWithoutAuthor> VideosPosted { get; set; }
        public AllFavoritesViewModel AllFavorites { get; set; }

        public StudentCompleteInfoViewModel(Guid id, string firstName, string lastName, string email, 
            string userName, string imageUrl, string telephone, int points, string scholarity, 
            string institution, bool isDeleted, bool isBlocked)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            ImageUrl = imageUrl;
            Telephone = telephone;
            Points = points;
            Scholarity = scholarity;
            Institution = institution;
            IsDeleted = isDeleted;
            IsBlocked = isBlocked;
        }

    }
}
