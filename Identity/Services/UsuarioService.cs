using AutoMapper;
using GerenciamentoTarefasApi.Services;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private ITokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }
            var usuario = _signInManager
               .UserManager
               .Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = "";
            var role = "";

            if (usuario != null) { 
                if (await _userManager.IsInRoleAsync(usuario, "User"))
                    role = "User";
            

                if (await _userManager.IsInRoleAsync(usuario, "Admin"))
                    role = "Admin";

                token = _tokenService.GenerateToken(usuario, role);
            }

            return token;
        }
    }
}