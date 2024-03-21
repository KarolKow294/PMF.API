namespace PMF.API.Models
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
