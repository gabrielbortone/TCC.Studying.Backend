using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Account
{
    public class RecoverPasswordCommand : CommandBase
    {
        public string UserName { get; private set; }

        [JsonConstructor]
        public RecoverPasswordCommand(string userName)
        {
            UserName = userName;
        }
    }
}
