using AMChat.Data;
using AMChat.Helpers;
using AMChat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colma2.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext db;
        private readonly UserManager<Usuario> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext Db,
            UserManager<Usuario> UserManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            db = Db;
            userManager = UserManager;

            this.roleManager = roleManager;
        }

        public Usuario GetUsuario()
        {
            string UserName = httpContextAccessor.HttpContext.User.Identity.Name;
            var usuario = db.Users.FirstOrDefault(a => a.Email == UserName);

            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public string NombreDeUsuario()
        {
            var persona = db.Users.FirstOrDefault(a => a.Email == GetUsuario().Email);

            if (persona == null)
            {
                return null;
            }

            return $"{persona.Nombres} {persona.Apellidos}";
        }

        public async Task<Response> CreateUserAsync(Usuario user, string PassWord)
        {
            try
            {
                var U = await userManager.FindByNameAsync(user.Email);

                if (U == null)
                {
                   var result = await userManager.CreateAsync(user, PassWord);

                    if (result.Succeeded)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = $"{user.Email} registrado con exito",
                            Result = user,

                        };
                    }
                    else
                    {
                        string errores = "";
                        foreach (var error in result.Errors)
                        {
                           errores += error.Description;
                        }

                        return new Response
                        {
                            IsSuccess = false,
                            Message = errores,
                            Result = user,

                        };
                    }
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = $"El usuario con el correo {user.Email} ya existe",
                    Result = U,
                };

            }
            catch (System.InvalidOperationException ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString(),

                };
            }
        }

        public async Task SetRoleToUserAsync(string userEmail, string Role)
        {
            try
            {
                var user = await userManager.FindByNameAsync(userEmail);

                if (!await userManager.IsInRoleAsync(user, Role))
                {
                    await userManager.AddToRoleAsync(user, Role);
                }
            }
            catch (System.InvalidOperationException)
            {


            }
        }

        public async Task SetRoleToUserAsync(List<string> usersEmail, string Role)
        {
            foreach (var userEmail in usersEmail)
            {
                try
                {
                    var user = await userManager.FindByNameAsync(userEmail);

                    if (!await userManager.IsInRoleAsync(user, Role))
                    {
                        await userManager.AddToRoleAsync(user, Role);
                    }
                }
                catch (System.InvalidOperationException)
                {


                }
            }
        }

        public List<string> GetUsersId(List<string> emails)
        {
            List<string> lista = new List<string>();

            foreach (var item in emails)
            {
                var user = userManager.FindByNameAsync(item);

                lista.Add(user.Id.ToString());
            }

            return lista;
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public List<IdentityRole> GetUserRoles(string Id)
        {
            var r = db.UserRoles.Where(a => a.UserId == Id).ToList();
            var identityRoles = roleManager.Roles;
            var roles = new List<IdentityRole>();

            foreach (var item in r)
            {

                var role = identityRoles.FirstOrDefault(a => a.Id == item.RoleId);

                if (role != null)
                {
                    roles.Add(role);
                }
            }

            return roles;
        }

        public async Task<Usuario> GetUserByIdAsync(string id)
        {
            return await userManager.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task DeleteRoleFromUserAsync(string userID, string RoleID)
        {
            try
            {
                var role = await roleManager.Roles.FirstOrDefaultAsync(a => a.Id == RoleID);

                var user = await userManager.FindByIdAsync(userID);

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            catch (System.InvalidOperationException)
            {

            }
        }

        public async Task<bool> IsInRole(Usuario usuario, string role)
        {
            return await userManager.IsInRoleAsync(usuario, role);
        }
    }
}
