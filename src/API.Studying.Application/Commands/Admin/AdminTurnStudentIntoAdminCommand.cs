using System;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminTurnStudentIntoAdminCommand : CommandBase
    {
        public Guid StudentId { get; set; }

        public AdminTurnStudentIntoAdminCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public override bool IsValid()
        {
            if (StudentId.Equals(Guid.Empty))
            {
                return false;
            }
            return true;
        }
    }
}
