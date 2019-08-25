using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.DataServices;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersRepository _repo;
        private IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public UsersController(IUsersRepository repository, IMapper mapper)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Model.UserModel))]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var userEntity = await _repo.GetUserAsync(Guid.Parse(id));

            if (userEntity == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Model.UserModel>(userEntity);

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Model.UserModel))]
        public async Task<IActionResult> AddUser([FromBody] Model.UserForCreation userToAdd)
        {
            var userEntity = _mapper.Map<Entities.User>(userToAdd);
            _repo.AddUser(userEntity);
            await _repo.SaveChangesAsync();
            var resultEntity = await _repo.GetUserAsync(userEntity.Id);
            var result = _mapper.Map<Model.UserModel>(resultEntity);


            return CreatedAtRoute("GetUser", new { id = result.Id }, result);

        }

        /// <summary>
        /// Authenticates a registered user.
        /// </summary>
        /// <param name="userCreds">User Credentials</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Model.UserModel))]
        public async Task<IActionResult> Authenticate([FromBody]Model.UserForAuthentication userCreds)
        {
            var user = await _repo.Authenticate(userCreds.UserEmail, userCreds.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            var result = _mapper.Map<Model.UserModel>(user);

            // return basic user info (without password)
            return Ok(result);
        }
    }
}