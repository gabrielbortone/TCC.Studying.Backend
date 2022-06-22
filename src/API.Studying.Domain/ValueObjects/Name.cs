using Microsoft.EntityFrameworkCore;

namespace API.Studying.Domain.ValueObjects
{
    [Owned]
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name(){}
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
