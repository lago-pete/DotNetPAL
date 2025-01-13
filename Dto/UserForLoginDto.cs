using System.ComponentModel.DataAnnotations;

namespace DotnetAPI.Dtos
{
    public partial class UserForLoginDto
    {
        public UserForLoginDto()
        {
            if (Email == null )
            {
                Email = " ";
            }
            if (Password == null )
            {
                Password = " ";
            }
            
        }



        [Required]
        public string  Email {get; set;}
        [Required]
        public string  Password {get; set;}




    }
}