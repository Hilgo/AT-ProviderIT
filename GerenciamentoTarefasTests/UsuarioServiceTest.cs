using AutoMapper;
using GerenciamentoTarefasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace GerenciamentoTarefasTests
{

    public class UsuarioServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<UserManager<Usuario>> _userManagerMock;
        private readonly Mock<SignInManager<Usuario>> _signInManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _userManagerMock = new Mock<UserManager<Usuario>>(
                Mock.Of<IUserStore<Usuario>>(),
                null, null, null, null, null, null, null, null
            );
            _signInManagerMock = new Mock<SignInManager<Usuario>>(
                _userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<Usuario>>(),
                null, null, null, null
            );
            _tokenServiceMock = new Mock<ITokenService>();

            _usuarioService = new UsuarioService(
                _mapperMock.Object,
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Fact]
        public async Task Login_UsuarioValido_RetornaToken()
        {
            // Arrange
            var usuario = new Usuario { UserName = "teste@example.com", Email = "teste@example.com" };
            var loginDto = new LoginUsuarioDto { Username = "teste@example.com", Password = "Password123!" };

            _userManagerMock.Setup(x => x.FindByNameAsync(loginDto.Username)).ReturnsAsync(usuario);
            _signInManagerMock.Setup(x => x.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false))
                .ReturnsAsync(SignInResult.Success);
            _userManagerMock.Setup(x => x.IsInRoleAsync(usuario, "User")).ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.GenerateToken(usuario, "User")).Returns("token_gerado");

            // Act
            var token = await _usuarioService.Login(loginDto);

            // Assert
            Assert.Equal("token_gerado", token);
        }

        [Fact]
        public async Task Login_UsuarioInvalido_LançaExcecao()
        {
            // Arrange
            var loginDto = new LoginUsuarioDto { Username = "invalido@example.com", Password = "Password123!" };

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _usuarioService.Login(loginDto));
        }
    }

}
