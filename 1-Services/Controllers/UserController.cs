using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Data.Interfaces;
using System;
using Newtonsoft.Json;


namespace MyCardsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        public UserController(ILogger<UserController> logger
            , IConfiguration config
            , IUserRepository userRepository
            )
        {
            _config = config;
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _userRepository.Get();
        }

        [HttpPost]
        public async Task<string> Post([FromBody] User user)
        {
            var msg = new Message<User>();
            try
            {
                msg.Data = await _userRepository.Post(user);
                msg.IsSuccess = true;               
                return JsonConvert.SerializeObject(msg);
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = ex.Message;
                return JsonConvert.SerializeObject(msg);
            }

        }
    }
}
