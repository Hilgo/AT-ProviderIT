﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService : ICadastroService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;

        public CadastroService(UserManager<Usuario> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
            else
            {
                // Atribua a role "User" por padrão
                await _userManager.AddToRoleAsync(usuario, "User");
            }

        }
    }
}
