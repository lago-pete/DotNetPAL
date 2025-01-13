using System.ComponentModel.DataAnnotations;


namespace DotnetAPI.Dtos
{
    public partial class UserForRegistrationDto
    {

        public UserForRegistrationDto()
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
            if (Password == null )
            {
                Password = " ";
            }
            if (PasswordConformation == null )
            {
                PasswordConformation = " ";
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
        public string  Password {get; set;}
        [Required]
        public string PasswordConformation {get; set;}
        




    }
}