using API.Studying.Domain.Entities;
using System;

namespace API.Studying.Application.DTOs
{
    public class TokenDto
    {
        public string TokenString { get; set; }
        public StudentDto Student { get; set; }
        public DateTime DateExpiration { get; set; }
        public TokenDto(string tokenString, StudentDto student, DateTime dateExpiration)
        {
            TokenString = tokenString;
            Student = student;
            DateExpiration = dateExpiration;
        }
    }
}
