using API.Studying.Domain.CustomExceptions;
using API.Studying.Domain.Entities.PreferencesUser.Favorites;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.ValueObjects;
using API.Studying.Domain.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Studying.Domain.Entities
{
    public class Student : Entity
    {
        public User IdentityUser { get; private set; }
        public Name Name { get; private set; }
        public Cpf CPF { get; private set; }
        public string UrlImage { get; private set; }
        public string Scholarity { get; private set; }
        public string Institution { get; private set; }
        public int Point { get; private set; }
        public bool IsBlocked { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsAdmin { get; private set; } = false;

        public ICollection<Document> Documents { get; private set; }
        public ICollection<Video> Videos { get; private set; }
        public ICollection<DocumentViewReport> DocumentViewReports { get; private set; }
        public ICollection<VideoViewReport> VideoViewReports { get; private set; }

        [NotMapped]
        public AllFavoritesViewModel AllFavorites { get; set; }

        public Student() { }
        public Student(User identityUser, Name name, Cpf cPF, string urlImage, 
            string scholarity, string institution, int point, 
            ICollection<Document> documents,ICollection<Video> videos, 
            ICollection<DocumentViewReport> documentViewReports,
            ICollection<VideoViewReport> videoViewReports
            )
        {
            IdentityUser = identityUser;
            Name = name;
            CPF = cPF;
            UrlImage = urlImage;
            Scholarity = scholarity;
            Institution = institution;
            Point = point;
            Documents = documents;
            Videos = videos;
            DocumentViewReports = documentViewReports;
            VideoViewReports = videoViewReports;

            if (!IsValid())
            {
                throw new DomainException(nameof(Student), nameof(this.Id), "Um ou mais erros encontrados na entidade!", nameof(this.IsValid));
            }
        }

        public Student(User identityUser, Name name, Cpf cPF, string urlImage, string scholarity, string institution, int point, bool isBlocked, bool isDeleted, ICollection<Document> documents, ICollection<Video> videos)
        {
            IdentityUser = identityUser;
            Name = name;
            CPF = cPF;
            UrlImage = urlImage;
            Scholarity = scholarity;
            Institution = institution;
            Point = point;
            IsBlocked = isBlocked;
            IsDeleted = isDeleted;
            Documents = documents;
            Videos = videos;

            if (!IsValid())
            {
                throw new DomainException(nameof(Student), nameof(this.Id), "Um ou mais erros encontrados na entidade!", nameof(this.IsValid));
            }
        }
        public void DeleteStudent()
        {
            if (IsDeleted)
            {
                IsDeleted = false;
            }
            else
            {
                IsDeleted = true;
            }
        }

        public void BlockStudent()
        {
            if (IsBlocked)
            {
                IsBlocked = false;
            }
            else
            {
                IsBlocked = true;
            }
        }

        public void TurnStudentIntoAdmin()
        {
            if (IsAdmin)
            {
                IsAdmin = false;
            }
            else
            {
                IsAdmin = true;
            }
        }

        public void UpdatePhoto(string urlImage)
        {
            this.UrlImage = urlImage;
        }

        public void UpdatePoints(int point)
        {
            Point += point;
            if(Point <= 0)
                Point = 0;
        }

        public void UpdateParams(Name name, string urlImage, string scholarity, string institution)
        {
            Name = name;
            UrlImage = urlImage;
            Scholarity = scholarity;
            Institution = institution;
        }

        public override bool IsValid()
        {
            var validator = new StudentValidator().Validate(this);
            if (!CPF.IsValid)
            {
                validator.Errors.Add(new ValidationFailure("", "Cpf Inválido!"));
            }
            return validator.IsValid;
        }
    }

    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(c => c.Name.FirstName)
                .MaximumLength(50)
                .WithMessage("Name should be less than 50");

            RuleFor(c => c.Name.FirstName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Name is required");

            RuleFor(c => c.Name.LastName)
                .MaximumLength(50)
                .WithMessage("LastName should be less than 50");

            RuleFor(c => c.Name.LastName)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("LastName is required");

            RuleFor(c => c.CPF.Number)
                .MaximumLength(11)
                .WithMessage("CPF should be less than 50");

            RuleFor(c => c.CPF.Number)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("CPF is required");

            RuleFor(c => c.Scholarity)
                .MaximumLength(45)
                .WithMessage("Scholarity should be less than 45");

            RuleFor(c => c.Scholarity)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Scholarity is required");

            RuleFor(c => c.Institution)
                .MaximumLength(50)
                .WithMessage("Institution should be less than 50");

            RuleFor(c => c.Institution)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("Institution is required");
        }
    }
}
