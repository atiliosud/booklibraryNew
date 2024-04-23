using BookLibrary.Commands;
using BookLibrary.DTOs;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using BookLibrary.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.EventHandlers
{
    public class SignInHandler : IRequestHandler<SignIn, SignInResponseDTO>
    {
        private readonly IUserRepository<User> _repositoryUser;
        private readonly JwtService _jwtService;

        public SignInHandler(IUserRepository<User> repositoryUser, JwtService jwtService)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<SignInResponseDTO> Handle(SignIn request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                var user = await _repositoryUser.GetUserByEmail(request.Email);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var token = await _repositoryUser.SignIn(request.Email, request.Password);

                user.Password = null;
                return new SignInResponseDTO
                {
                    User = user,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                // Aqui você pode lidar com diferentes tipos de exceções e retornar respostas adequadas.
                // Por exemplo, para uma exceção de usuário não encontrado, você pode retornar um objeto SignInResponseDTO com User = null e Token = null.
                return new SignInResponseDTO
                {
                    User = null,
                    Token = null,
                    ErrorMessage = ex.Message // Adicione a mensagem de erro à resposta
                };
            }
        }
    }
}
