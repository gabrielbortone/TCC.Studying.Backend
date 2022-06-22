using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Account
{
    public class RecoverPasswordConfirmedCommand : CommandBase
    {
        public string UserName { get; set; }
        public string Number { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        
        [JsonConstructor]
        public RecoverPasswordConfirmedCommand(string userName, string number, string newPassword, string confirmNewPassword)
        {
            UserName = userName;
            Number = number;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }
}
