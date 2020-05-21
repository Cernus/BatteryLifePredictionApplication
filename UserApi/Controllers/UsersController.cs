using UserApi.Models;
using UserApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [Route("api/users/{action}")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserRepository _userRepository { get; set; }

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/Users/Create
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] UserDto user)
        {
            User entity = await _userRepository.CreateUser(user);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // GET: api/Users/Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Details()
        {
            List<UserDto> batteries = await _userRepository.GetUsers();

            if (batteries == null)
            {
                return NotFound();
            }

            return Ok(batteries);
        }

        // GET: api/Users/Detail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Detail(int id)
        {
            UserDto user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/Update
        public async Task<ActionResult<User>> Update([FromBody] UserDto user)
        {
            User entity = await _userRepository.UpdateUser(user);

            if (entity == null)
            {
                return NotFound();
            }

            if (user.UserId != entity.UserId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        // PUT: api/Users/Delete/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User entity = await _userRepository.DeleteUser(id);

            if (entity == null)
            {
                return NotFound();
            }

            if (id != entity.UserId)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        // POST api/Users/getuser
        [HttpPost]
        public async Task<UserDto> GetUser([FromBody]List<string> authDetails)
        {
            UserDto user = await _userRepository.GetUser(authDetails[0], authDetails[1]);

            return user;
        }

        // GET api/Users/IsActive/5
        [HttpGet("{id}")]
        public async Task<ActionResult> IsActive(int id)
        {
            if (await _userRepository.IsActive(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Users/UsernameExists
        public async Task<ActionResult<User>> UsernameExists([FromBody] string username)
        {
            bool? result = await _userRepository.UsernameExists(username);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
