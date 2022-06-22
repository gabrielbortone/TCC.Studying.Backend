using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Account
{
    public class ChangePasswordCommand : CommandBase
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        [JsonConstructor]
        public ChangePasswordCommand(string userName, string oldPassword, string newPassword, string confirmNewPassword)
        {
            UserName = userName;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }
}
