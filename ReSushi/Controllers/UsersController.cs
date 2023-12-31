﻿using Microsoft.AspNetCore.Mvc;
using ReSushi.Models;
using ReSushi.Repository;

namespace ReSushi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppAuthService _appAuthService;

        public UsersController(IAppAuthService appAuthService)
        {
            _appAuthService = appAuthService;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(User user)
        {
            var token = await _appAuthService.Authenticate(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
