using API.Studying.Domain.Entities;
using API.Studying.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace API.Studying.Domain.Interfaces.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Student GetById(Guid id);
        Student GetByCpf(string numberOfCpf);
        StudentCompleteInfoViewModel GetByIdAllInfo(Guid id);
        List<Student> GetAll();
        List<Student> GetAllExceptAdmins();
        List<Student> GetBySearch(string key);
        void DeleteStudent(Guid studentId);
        void BlockStudent(Guid studentId);
        List<Student> GetRaking(int top);
    }
}
