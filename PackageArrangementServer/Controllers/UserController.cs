using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Services;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private IUserService userService;

        public UserController(IUserService us) { userService = us; }

        private bool Validate(string id, string password)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password)) return false;

            User user = userService.Get(id);
            if (user == null) return false;

            return user.Password == password;
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>List<User></returns>
        [HttpGet]
        public List<User> Get()
        {
            return userService.GetAllUsers();
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] LoginRequest req)
        {
            throw new NotImplementedException(); // jwt?
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("{id}/{name}/{password}")]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody] RegisterRequest req)
        {
            throw new NotImplementedException();
        }

    }
}
