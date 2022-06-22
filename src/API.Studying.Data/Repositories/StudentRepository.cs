using API.Studying.Data.DbContext;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Studying.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;

        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BlockStudent(Guid studentId)
        {
            var student = GetById(studentId);
            if (student == null)
            {
                throw new ArgumentException("Esse estudante não existe na base de dados!");
            }
            if (student.IsAdmin && !student.IsBlocked)
            {
                throw new ArgumentException("Esse estudante é admin, portanto não pode ser alterado!");
            }
            student.BlockStudent();
            _dbContext.Student.Update(student);
        }

        public void Create(Student entity)
        {
            _dbContext.Student.Add(entity);
        }

        public void Delete(Guid id, Guid studentId)
        {
            if (id != studentId)
                throw new ArgumentException("Impossível deletar dados de outro usuário!");

            var student = GetById(id);


            if (student != null && !student.IsAdmin)
                _dbContext.Student.Remove(student);
        }

        public void DeleteStudent(Guid studentId)
        {
            var student = GetById(studentId);
            if(student == null)
            {
                throw new ArgumentException("Esse estudante não existe na base de dados!");
            }
            if (student.IsAdmin)
            {
                throw new ArgumentException("Esse estudante é admin, portanto não pode ser alterado!");
            }
            student.DeleteStudent();
            _dbContext.Student.Update(student);
        }

        public List<Student> GetAll()
        {
            var students = _dbContext.Student.AsQueryable().Include(s => s.IdentityUser).Where(s=> s.IsDeleted==false).ToList();
            foreach(var student in students)
            {
                student.IdentityUser.UpdateParams(student.Id, null);
            }
            return students;
        }

        public List<Student> GetAllExceptAdmins()
        {
            var students = _dbContext.Student.AsQueryable().Include(s => s.IdentityUser).Where(s=> s.IsDeleted == false && !s.IsAdmin).ToList();
            foreach (var student in students)
            {
                student.IdentityUser.UpdateParams(student.Id, null);
            }
            return students;
        }

        public Student GetByCpf(string numberOfCpf)
        {
            return _dbContext.Student.AsQueryable().FirstOrDefault(s => s.CPF.Number.Equals(numberOfCpf));
        }

        public Student GetById(Guid id)
        {
            return _dbContext.Student.AsQueryable()
                .Include(s=> s.Documents)
                .Include(s=> s.Videos)
                .Include(s=> s.IdentityUser)
                .FirstOrDefault(s => s.Id.Equals(id));
        }

        public StudentCompleteInfoViewModel GetByIdAllInfo(Guid id)
        {
            var student = _dbContext.Student
                .Include(s=> s.IdentityUser)
                .Select(s=> new StudentCompleteInfoViewModel(s.Id, s.Name.FirstName, s.Name.LastName, s.IdentityUser.Email, s.IdentityUser.UserName, s.UrlImage, s.IdentityUser.PhoneNumber, s.Point, s.Scholarity, s.Institution, s.IsDeleted, s.IsBlocked))
                .AsEnumerable()
                .FirstOrDefault(s => s.Id.Equals(id));

            var documentPosted = _dbContext.Document
                .Include(d => d.Student)
                .Include(d => d.Topic)
                .Where(d => d.Student.Id.Equals(id))
                .Select(d => new DocumentViewModelWithoutAuthor(d.Id, d.Title, d.UrlDocument,
                d.Stars, d.Keys, d.Views, new TopicViewModel(d.Topic.Id, d.Topic.Title, d.Topic.Description)))
                .ToList();
            
            var videosPosted = _dbContext.Video
                .Include(v=> v.Student)
                .Include(v=> v.Topic)
                .Where(v=> v.Student.Id.Equals(id))
                .Select(v=> new VideoViewModelWithoutAuthor(v.Id, new TopicViewModel(v.Topic.Id, v.Topic.Title, v.Topic.Description),
                v.Order, v.Title, v.UrlVideo, v.Star, v.Keys, v.Views, v.Thumbnail))
                .ToList();

            student.DocumentsPosted = documentPosted;
            student.VideosPosted = videosPosted;

            return student;
        }

        public List<Student> GetBySearch(string key)
        {
            var students =  _dbContext.Student.AsQueryable().Include(s=> s.IdentityUser)
                .Where(s => (s.Name.FirstName.Contains(key) || s.Name.LastName.Contains(key) || 
                (s.Name.FirstName + s.Name.LastName).Contains(key) ||
                s.CPF.Number.Contains(key) ||
                s.IdentityUser.UserName.Contains(key) ||
                s.IdentityUser.Email.Contains(key)) && s.IsDeleted == false)
                .ToList();

            foreach (var student in students)
            {
                student.IdentityUser.UpdateParams(student.Id, null);
            }

            return students;
        }

        public List<Student> GetRaking(int top)
        {
            return _dbContext.Student.Include(s=> s.IdentityUser).OrderBy(s => s.Point).Take(top).ToList();
        }

        public void Update(Student entity)
        {
            _dbContext.Student.Update(entity);
        }
    }
}
