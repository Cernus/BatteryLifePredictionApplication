namespace UserApi.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string EmailAddress { get; set; }
    }
}
