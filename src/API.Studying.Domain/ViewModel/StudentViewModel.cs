using System;

namespace API.Studying.Domain.ViewModel
{
    public class StudentViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string ImageUrl { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsBlocked { get; private set; }
        public StudentViewModel(Guid id, string name, string lastName, string imageUrl, bool isDeleted, bool isBlocked)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            ImageUrl = imageUrl;
            IsDeleted = isDeleted;
            IsBlocked = isBlocked;
        }
    }
}
