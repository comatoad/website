﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Twitch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Users.Application.Commands.Auth;
using Momentum.Users.Core.Services;

namespace Momentum.Users.Api.Controllers.Auth
{
    [Route("auth/twitch")]
    [ApiController]
    public class TwitchController : Controller
    {
        private readonly IMediator _mediator;
        public TwitchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Twitch", Policy = "RequireNothing")]
        [HttpGet]
        public async Task<IActionResult> SignInAsync()
        {
            if (User.Identity == null ||
                !User.Identity.IsAuthenticated)
                return Challenge("Twitch");

            await _mediator.Send(new CreateOrUpdateUserTwitchCommand
            {
                DisplayName = User.Claims.First(x => x.Type == TwitchAuthenticationConstants.Claims.DisplayName).Value,
                TwitchId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value)
            });

            // Twitch auth is opened in a new window,
            // and the client waits till the window is closed before continuing
            return Content("<script>window.close();</script>", "text/html");
        }

        [HttpDelete("/api/user/profile/social/twitch")]
        public async Task<IActionResult> UnlinkAsync()
        {
            await _mediator.Send(new UnlinkUserTwitchCommand());

            return Ok();
        }
    }
}