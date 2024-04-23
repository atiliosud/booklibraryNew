using BookLibrary.DTOs;
using BookLibrary.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Commands
{
    /// <summary>
    /// Command to sign in the user in the library.
    /// </summary>
    public class SignIn : IRequest<SignInResponseDTO>
    {
        [Required]
        [StringLength(100)]
        public string Email { get; }

        [Required]
        [StringLength(100)]
        public string Password { get; }

        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
