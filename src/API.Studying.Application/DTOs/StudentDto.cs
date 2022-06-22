using System;

namespace API.Studying.Application.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdmin { get; set; }
        public int Points { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }
        public StudentDto(Guid id, string name, string lastName, string imageUrl, bool isAdmin, 
            int points, string email, string userName, bool isDeleted, bool isBloqued)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            ImageUrl = imageUrl;
            IsAdmin = isAdmin;
            Points = points;
            Email = email;
            UserName = userName;
            IsDeleted = isDeleted;
            IsBlocked = isBloqued;
        }
    }
}
