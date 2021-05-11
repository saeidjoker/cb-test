using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : BaseController
    {
        // GET: api/ping
        [HttpGet]
        public string Get()
        {
            return "Ping received";
        }

        // GET api/ping/with-auth
        [HttpGet("with-auth")]
        [Authorize]
        public string GetWithAuth()
        {
            return $"Ping received with successful authorization. User Name : {GetUserName()}";
        }
    }
}
