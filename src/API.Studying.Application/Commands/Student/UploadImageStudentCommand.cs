using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Student
{
    public class UploadImageStudentCommand : CommandBase
    {
        public string ImageStringBase64 { get; private set; }
        public Guid StudentId { get; set; }

        [JsonConstructor]
        public UploadImageStudentCommand(string imageStringBase64)
        {
            ImageStringBase64 = imageStringBase64;
        }
    }
}
