using System.Linq;
using EmployeeTaskManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManagement.Controllers
{
    [Authorize]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers jwt;

        public TokenController(JwtHelpers jwt)
        {
            this.jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost("~/SignIn")]
        public ActionResult<string> SignIn()
        {
            return jwt.GenerateToken("TestUser");
        }
    }
}