using BookLibrary.Entities;

namespace BookLibrary.DTOs
{
    public class SignInResponseDTO
    {
        public User User { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
