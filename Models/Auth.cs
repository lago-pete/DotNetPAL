using System.ComponentModel.DataAnnotations;

namespace DotnetAPI.Models
{
    public partial class Auth
    {
        public Auth()
        {
            if (Email == null)
            {
                Email = " ";
            }

        }

        
        public string Email {get;set;}
        
        public byte[] PasswordHash {get; set;}
        
        public byte[] PasswordSalt {get; set;}








    }
}