using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMChat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMChat.API
{
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers(string id)
        {
            return Ok(new { users = dbContext.Users.Where(a=> a.Id != id).OrderBy(a => a.Nombres) });
        }
    }
}
