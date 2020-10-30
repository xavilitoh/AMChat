using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AMChat.API.Models;
using AMChat.Data;
using AMChat.Helpers;
using AMChat.Models;
using Colma2.Helpers;
using Colma2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Colma2.API
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;
        private readonly IUserHelper userHelper;
        private readonly IFileHelper fileHelper;
        private readonly IFacebookAuthService fb;

        public AccountApiController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IUserHelper userHelper,
            IFileHelper fileHelper,
            IFacebookAuthService fb)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            this.context = context;
            this.userHelper = userHelper;
            this.fileHelper = fileHelper;
            this.fb = fb;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model, IFormFile file)
        {

            Usuario user = model;
            user.UserName = user.Email;

            //user.FotoUrl = fileHelper.UploadPhoto(null, "user");

            var result = await userHelper.CreateUserAsync(user, model.Password);

            if (result.IsSuccess)
            {
                await CreateToken(user.Id, model.Token);

                return BuildToken(result);

            }
            else
            {
                return BadRequest(new Response { Message = result.Message, IsSuccess = false });
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    try
                    {
                        var user = context.Users.FirstOrDefault(a => a.Email == userInfo.Email);

                        await CreateToken(user.Id, userInfo.Token);

                        return BuildToken(new Response { IsSuccess = true, Result = user });
                    }
                    catch (Exception ex)
                    {

                        return Ok(ex);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("LoginWithFb")]
        public async Task<IActionResult> LoginWithFb([FromBody] LoginFB loginFB)
        {
            var validationTokenResult = await fb.ValidateAccessAsync(loginFB.fbToken);

            if (!validationTokenResult.Data.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            var userInfo = await fb.GetUserInfoAsync(loginFB.fbToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            using (var transaccion = context.Database.BeginTransaction())
            {

                try
                {
                    if (user == null)
                    {
                        user = new Usuario
                        {
                            Email = userInfo.Email,
                            Nombres = userInfo.FirstName,
                            Apellidos = userInfo.LastName,
                            Id = Guid.NewGuid().ToString(),
                            UserName = userInfo.Email

                        };

                        var createResult = await _userManager.CreateAsync(user);

                        await userHelper.CreateUserAsync(user, "Abcd1234.");

                    }

                    await CreateToken(user.Id, loginFB.token);

                    var bytes = fileHelper.GetBytesFromUrl(userInfo.Picture.Data.Url.ToString());

                    user.FotoUrl = fileHelper.WriteBytesToFile($"{user.FullName}.jpg", bytes);
                    context.Update(user);

                    context.Update(user);
                    await context.SaveChangesAsync();

                    await transaccion.CommitAsync();

                    return BuildToken(new Response { IsSuccess = true, Result = user });
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }

            }

        }

        private async Task CreateToken(string userId, string token)
        {
            var Token = await context.Tokens.FirstOrDefaultAsync(a => a.Token == token);

            if (Token == null)
            {
                Token = new Tokens
                {
                    Token = token,
                    UltimoUso = DateTime.Now,
                    UsuarioId = userId
                };

                context.Add(Token);
                await context.SaveChangesAsync();
            }
            else
            {
                Token.UsuarioId = userId;
                Token.UltimoUso = DateTime.Now;

                context.Update(Token);
                await context.SaveChangesAsync();
            }


        }


        [Route("deleteToken")]
        [HttpPost]
        public async Task<JsonResult> DeleteToken([FromBody] Tokens data)
        {
            var Token = await context.Tokens.FirstOrDefaultAsync(a => a.Token == data.Token);

            context.Tokens.Remove(Token);
            await context.SaveChangesAsync();

            return Json("Ok");

        }

        private IActionResult BuildToken(Response response)
        {
            var userInfo = response.Result as Usuario;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lkjshdfsjdvlknxkcvnhxklvzhdjkfngjhfvzxgfduzbudyfusbfyzsudfybzsudfbzxsudfysenvurytsuevrtyserutsyvber"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "iasdchm.com",
               audience: "iasdchm.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration,
                nombre = $"{userInfo.Nombres} {userInfo.Apellidos}",
                id = userInfo.Id,
                foto = userInfo.FotoUrl,
            });

        }
    }
}