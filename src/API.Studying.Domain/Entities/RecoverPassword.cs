using System;

namespace API.Studying.Domain.Entities
{
    public class RecoverPassword : Entity
    {
        public string Code { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public DateTime Date { get; private set; }
        public RecoverPassword(string code, string email, string userName, DateTime date)
        {
            Code = code;
            Email = email;
            UserName = userName;
            Date = date;
        }
    }
}
