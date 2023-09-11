namespace PhoneBook.Application.DTOs.Account
{
    public class ChangepasswordDTO
    {

        public string UserId { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
