using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.AccountDTOS;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,
        SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Registerdto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();

                    var appUser = new AppUser
                    {
                        UserName = registerDto.UserName,
                        Email = registerDto.Email
                    };

                    var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                    if(createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                        if(roleResult.Succeeded)
                        {
                            return Ok(
                                new NewUserdto
                                {
                                    Name = appUser.UserName,
                                    Email = appUser.Email,
                                    Token = _tokenService.CreateToken(appUser)
                                }
                            );
                        }

                        else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }    
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("login")]
        public async Task<IActionResult> Login(Logindto logindto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == logindto.UserName.ToLower());

            if(user == null) return Unauthorized("Invalid username!");

            var response = await _signinManager.CheckPasswordSignInAsync(user, logindto.Password, false);

            if(!response.Succeeded) return Unauthorized("Username not found and/or password incorrect!");

            return Ok(
                new NewUserdto
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );

        }
    }
}