using AMChat.Models;
using Colma2.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public interface IUserHelper
    {
        string NombreDeUsuario();
        Usuario GetUsuario();
        Task<Response> CreateUserAsync(Usuario user, string PassWord);
        Task SetRoleToUserAsync(string userEmail, string Role);
        Task SetRoleToUserAsync(List<string> userEmails, string Role);
        List<string> GetUsersId(List<string> emails);
        Task<bool> IsInRole(Usuario usuario, string role);
        Task DeleteRoleFromUserAsync(string userID, string RoleID);
        Task<Usuario> GetUserByIdAsync(string id);
        Task<List<IdentityRole>> GetRoles();
        List<IdentityRole> GetUserRoles(string Id);
    }
}
