using System.ComponentModel.DataAnnotations;

namespace DotnetApi.Dtos
{
    public partial class UserForConfirmationDto
    {

        public UserForConfirmationDto()
        {
            if (PasswordHash == null)
            {
                PasswordHash = new byte[0];
            }
            if (PasswordSalt == null)
            {
                PasswordSalt = new byte[0];
            }

        }

        
        [Required]
        public byte[] PasswordHash {get; set;}
        [Required]
        public byte[] PasswordSalt {get; set;}





    }
}