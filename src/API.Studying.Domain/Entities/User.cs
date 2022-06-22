using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.Studying.Domain.Entities
{
    public class User : IdentityUser
    {
        [ForeignKey("Student")]
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }

        public void UpdateParams(Guid studentId, Student student)
        {
            StudentId = studentId;
            Student = student;
        }
    }
}