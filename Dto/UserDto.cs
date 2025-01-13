using System.ComponentModel.DataAnnotations;

namespace DotnetAPI.Dto
{
    public partial class UserToAddDto{

        public UserToAddDto()
        {
            if (FirstName == null)
            {
                FirstName = " ";
            }
            if (LastName == null)
            {
                LastName = " ";
            }
            if (Email == null)
            {
                Email = " ";
            }
            if (Gender == null)
            {
                Gender = " ";
            }
           
        }


        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get;set;}
        [Required]
        public string Email {get;set;}
        [Required]
        public string Gender {get;set;}
        [Required]
        public bool Active {get;set;}
    }
}