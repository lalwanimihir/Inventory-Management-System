namespace Inventory_Management_System.Application.Dtos.AuthDto.ResponseDtos
{
    public class LoginResponseDto
    {
        public bool IsLoggedIn { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Role { get; set; }
    }
}
