using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Contents;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using OrchardCore.Users.ViewModels;

namespace OrchardCore.Content.Controllers
{
    [Route("api/content/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken]
    public class ApiController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IContentItemIdGenerator _idGenerator;
        private readonly IUserService _userService;

        public ApiController(
            IContentManager contentManager,
            IAuthorizationService authorizationService,
            IContentItemIdGenerator idGenerator,
            IUserService userService)
        {
            _authorizationService = authorizationService;
            _contentManager = contentManager;
            _idGenerator = idGenerator;
            _userService = userService;
        }

        [Route("{contentItemId}"), HttpGet]
        public async Task<IActionResult> Get(string contentItemId)
        {
            var contentItem = await _contentManager.GetAsync(contentItemId);

            if (contentItem == null)
            {
                return NotFound();
            }

            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ViewContent, contentItem))
            {
                return Unauthorized();
            }

            return Ok(contentItem);
        }

        [HttpDelete]
        [Route("{contentItemId}")]
        public async Task<IActionResult> Delete(string contentItemId)
        {
            var contentItem = await _contentManager.GetAsync(contentItemId);

            if (contentItem == null)
            {
                return StatusCode(204);
            }

            if (!await _authorizationService.AuthorizeAsync(User, Permissions.DeleteContent, contentItem))
            {
                return Unauthorized();
            }

            await _contentManager.RemoveAsync(contentItem);

            return Ok(contentItem);
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        //[ActionName("Post")]
        public async Task<IActionResult> Post(ContentItem newContentItem, bool draft = false)
        {
            var contentItem = await _contentManager.GetAsync(newContentItem.ContentItemId, VersionOptions.DraftRequired);

            if (contentItem == null)
            {
                if (!await _authorizationService.AuthorizeAsync(User, Permissions.PublishContent))
                {
                    return Unauthorized();
                }

                if (String.IsNullOrEmpty(newContentItem.ContentItemId))
                {
                    newContentItem.ContentItemId = _idGenerator.GenerateUniqueId(newContentItem);
                }

                await _contentManager.CreateAsync(newContentItem, VersionOptions.DraftRequired);

                contentItem = newContentItem;
            }
            else
            {
                if (!await _authorizationService.AuthorizeAsync(User, Permissions.EditContent, contentItem))
                {
                    return Unauthorized();
                }
            }


            if (contentItem != newContentItem)
            {
                contentItem.DisplayText = newContentItem.DisplayText;
                contentItem.ModifiedUtc = newContentItem.ModifiedUtc;
                contentItem.PublishedUtc = newContentItem.PublishedUtc;
                contentItem.CreatedUtc = newContentItem.CreatedUtc;
                contentItem.Owner = newContentItem.Owner;
                contentItem.Author = newContentItem.Author;

                contentItem.Apply(newContentItem);

                await _contentManager.UpdateAsync(contentItem);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!draft)
            {
                await _contentManager.PublishAsync(contentItem);
            }

            return Ok(contentItem);
        }

        [HttpPost]
        [ActionName("Post02")]
        [EnableCors("MyPolicy"), AllowAnonymous]
        public async Task<IActionResult> Post02(RegisterApiViewModel model)
        {            
            var roleNames = new List<string>();

            if (model.IsFluencer)
            {
                roleNames.Add("Influencer");
            }

            if (model.IsBrand)
            {
                roleNames.Add("Brand");
            }

            var user = await _userService.CreateUserAsync(new User { UserName = model.UserName, Email = model.Email, EmailConfirmed = true, RoleNames = roleNames }, model.Password, (key, message) => ModelState.AddModelError(key, message)) as User;

            if (user != null)
            {
                return Ok(model.UserName);
            }
            else
            {
                return StatusCode(204);
            }
        }

        [HttpPost]
        [ActionName("Post03")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> Post03(ContentItem newContentItem, bool draft = false)
        {
            var contentItem = await _contentManager.GetAsync(newContentItem.ContentItemId, VersionOptions.DraftRequired);

            if (contentItem == null)
            {
                return StatusCode(204);
            }
            else
            {
                if (!await _authorizationService.AuthorizeAsync(User, Permissions.EditOwnContent, contentItem))
                {
                    return Unauthorized();
                }
            }

            if (contentItem != newContentItem)
            {
                contentItem.DisplayText = newContentItem.DisplayText;
                contentItem.ModifiedUtc = newContentItem.ModifiedUtc;
                contentItem.PublishedUtc = newContentItem.PublishedUtc;
                contentItem.CreatedUtc = newContentItem.CreatedUtc;
                contentItem.Owner = newContentItem.Owner;
                contentItem.Author = newContentItem.Author;

                contentItem.Apply(newContentItem);

                await _contentManager.UpdateAsync(contentItem);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!draft)
            {
                await _contentManager.PublishAsync(contentItem);
            }

            return Ok(contentItem);
        }
    }
}
