﻿using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momentum.Auth.Application.Commands;
using Momentum.Auth.Core.Services;

namespace Momentum.Auth.Api.Controllers
{
    [Route("auth/twitter")]
    [ApiController]
    public class TwitterController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public TwitterController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [Authorize(AuthenticationSchemes = "Twitter", Policy = "RequireNothing")]
        [HttpGet]
        public async Task<IActionResult> SignInAsync()
        {
            if (User.Identity == null ||
                !User.Identity.IsAuthenticated)
                return Challenge("Twitter");

            await _mediator.Send(new CreateOrUpdateUserTwitterCommand
            {
                DisplayName = User.Claims.First(x => x.Type == ClaimTypes.Name).Value
            });

            // Twitter auth is opened in a new window,
            // and the client waits till the window is closed before continuing
            return Content("<script>window.close();</script>", "text/html");
        }

        [HttpDelete("/api/user/profile/social/twitter")]
        public async Task<IActionResult> UnlinkAsync()
        {
            await _mediator.Send(new UnlinkUserTwitterCommand());

            return Ok();
        }
    }
}