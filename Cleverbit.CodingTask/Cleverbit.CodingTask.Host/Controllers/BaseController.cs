using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Cleverbit.CodingTask.Host.Controllers {
    public class BaseController : ControllerBase {
        protected int GetUserId() {
            return int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
        }

        protected string GetUserName() {
            return User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        }
    }
}
