using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Models;
using OrchardCore.Contents;
using OrchardCore.Contents.Models;
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
        static readonly HttpClient Client = new HttpClient();
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

        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                //string content = JsonConvert.SerializeObject(data);
                //var buffer = Encoding.UTF8.GetBytes(content);
                //var byteContent = new ByteArrayContent(buffer);
                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Client.DefaultRequestHeaders = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, "CfDJ8HC071pjUltCtZVltlPZ2wVnygv3jHVh_Ws7ml06vXtwkBpJh8AvynJV9MuPBx2lo5IPHc5ZJRUQCgqSfEMDMwO1ldMtV4DL0R38bhlhxC687bgG2nxwxfwamCs-t3WlMQ1vR9E0n_IVhd-RELF5pWEffRlRTkhKSCDPjpc-ECLw7HfFKJrL8rnkLYnEy-BKbDP___fithOYWR9IYkLxk_-hGSUbXQI-BTSSUe1c6PIdSvWa0K3XPlwuzDnFWby7Ds1zwm5IrrictecnjsHqjfnypN45Z4o7wku9BnobIJeOZ7U9eaj-dntclXxp9wa5gNg7wJvvMl4Jd4VTJUlPsu7lJlhqApJKH7YZdP3IlsuHO1jareozQk6kevoFwSs7vD6hGMY4rBclwmv1C8GE5VHo2f8PQWqvyvUWjaKx-AggvdfIY5jk7e6dXeHVyIBY2NlkZLfj59ZmwWQAaUJ8dTJdikyFg473jnyjAyjJ6qnKt4jTuHzyfxexocyrEtYItmbrT37Dsp8YJJRU8qAInvX81LTSQacfjnaRXm2l5Yr9Js8TcXvOD5thZ-Jojg0hcv72-2HMJE-oRj9TgAXH976U6ZVhLKEq5x01_tg5Kdx_0sWsuHsQLKjx8KpZB1xQzCPVnTf4yNegqJIw-XWzmYhQ2NR0P4GOc030X-qoauBKN9LU7JIntddhkLwbZakfeh9o3rxsR7ILqkGLS6mYKVfqbLjxlLSgcphlOa1o-pjmOqvNfZJ00Fj33D3xTJEP4DgkOOmT_BYIM5lGtwxUxRzLuokXNc-53uJCBaVk__uOjXFnUhFcRRPJPSeH79ZGN-8tTYx_f167pe95BQTN2U_HZwu_apgb2U9nNmHHINueNGKyt0_jsfHGJW0O7MkTqcY4nkNK8nTKmTZyZGJgmhYJ4uN1cDqKSx_4Hb8nWlluxV01YgB52YjHNf5nrlV0H2d6vluOGSBuOxTcbqXe9AN-zes7M2rM6MVLgk47-kDyywdzPsosvEAeJqdeUXO1mnvZo25FaXLAKKT63j-tGDiH0NiOVNPTgGbBaK9kC6gi-rSUdbWRY-oYya06lWb5RLrBkiP_C_pbTo_Lcf-L_9NZ9o9yeNaC4mwEkmV8BzfhS-IrzSBQ1ahhxleNwGMOUzo623Cy4ctNQ_zJ1wQ4dcYR2FnLc2MbjdEMj1TrPxFJ6xY5iuqUrdmBoNMjlb1gHWW8aDXL5gsrtquhW7qUq88ZSPT8oVyb0gM1FfpXWxsHMiXsxqkU8q1-4WFRLuMzi_6OWVcM6jmN5MuiqlKoUF9tpaJzkoBjbbQGxSKQEmi69nzko9627WuKjhQvtCU8w6JKsYzLO5x35a8uT1-Lzf_eQ8ERLOeeIJWtEti4Kh-tywPQMpnomSXuinz06FqJRLjZAEN3XRZze9Idi6aiFJj_l-oMwbl552IUaYKsTFNANBllPpupZ-pJcIjcstE_AjpMIfAcywrkQi5Hb7vSe5G4kqScGniMGk9_8NeFDmrAGV2FaEBZfiyfPeZfrtJPDbXwIbEXlcUbYFB4cSQQEt7-Al04iYxWyaTu4j5xb3NyCa4HPnqS1hrsCW9svb6PwqKIincfg8hMaz5kGxjFZB0Nu8v1y9sPAebSQbxSIlStvDdshVpct17pJ-Iqlz1czHmu6SYq-YeDyLFEsCJyAtyILtISYjvy_g0xnYFFzeW1pUv_hSIdMUhPhHa8zijnwRv-INmDCNs_Um1pOVhSlJByIKgZfgWFAyfYTM_Kp5AexKD82miy_QM_3KKEibVoAccivyRLvzGiodMMRj2pHehTzHdtR7kjZ0vTjn75ed9fZ9vfN2d0JSnOMeO7iFUaH9FHCWxOXh_VsYfFOavlGoQQDmfaWghhTELypv87Mbw0SH3kN28qs4wa0T_yzhDRKXfdNk5BUq1YvDMCc3EVci7NNBrsj1BvFvB4RaJEMoXv5sFWw2LLLpm2fLmFsarLfZl3nTLGixM377ZdUI5b-TcB29826eqB_FipVxSySonAiYamMrRFIbQXEsbC5j4iWPnXg-lghNEv72xi9tw8-mMVJ6282bU-WhLp477intuknXDH-iau2iFp3hx9cQVcNw_xQxznX78toPm7C_iHxPj4z6eyNAjnrXu8IIA5cG_rCYcwCMSwwOlsqF5yAZQj5OzJNQ45Lwu7qRNS2-zNou5Tj5qs3qe9roqFB3yrKeCQtTn5H_x2FKggnnezmhaQIxgJfRhnrb-lYUYWs-gHg_DETNqrxNw5-M3dripeU7VGdF09FWB-Vjyei3psMH-ASsAjxlsmQcybRjwiXgGzO02syNQK4dYOZvGIyQQh790eyYH7I5pdnp4wMVbdImdHG4snzyldFXMFp5Rl5jSv-1k");
                var response = await Client.GetAsync(url).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //logger.Error($"GetAsync End, url:{url}, HttpStatusCode:{response.StatusCode}, result:{result}");
                    //return new T();
                }
                //logger.Info($"GetAsync End, url:{url}, result:{result}");
                //return JsonConvert.DeserializeObject<T>(result);

                return response;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new System.Exception($"response :{responseContent}", ex);
                }
                throw;
            }
        }

        [Route("{contentItemId}"), HttpGet]
        [EnableCors("MyPolicy")]
        [ActionName("GetContent")]
        public async Task<IActionResult> GetContent(string contentItemId)
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

        [Route("{username}/{password}"), HttpGet]
        [EnableCors("MyPolicy"), AllowAnonymous]
        [ActionName("GetToken")]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return StatusCode(204);
            }
            else
            {
                var url = "https://www.h3win.net/getPTToken.php?username=" + username + "&password=" + password;
                var response = await GetAsync(url);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var ptModel = new PTModel
                    {
                        Errors = "Can not get Token from h3",
                        Result = response.Content.ToString(),
                        sts = 1
                    };

                    var json = JsonConvert.SerializeObject(ptModel);

                    return Ok(json);
                }
                else
                {
                    var ptModel = new PTModel
                    {
                        Errors = "0",
                        Result = response.Content.ToString(),
                        sts = 0
                    };

                    var json = JsonConvert.SerializeObject(ptModel);

                    return Ok(json);
                }
            }
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
        [ActionName("Post")]
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
                return Ok(user);
            }
            else
            {
                return StatusCode(204);
            }
        }

        //[HttpPost]
        //[ActionName("RegisterBrand")]
        //[EnableCors("MyPolicy"), AllowAnonymous]
        //public async Task<IActionResult> RegisterBrand(ContentItem newContentItem, bool draft = false)
        //{
        //    var roleNames = new List<string>();

        //    if (!String.IsNullOrEmpty(newContentItem.DisplayText) && newContentItem.ContentType == "Brand")
        //    {
        //        roleNames.Add("Brand");
        //    }

        //    var user = await _userService.CreateUserAsync(new User { UserName = newContentItem.DisplayText, Email = newContentItem.DisplayText, EmailConfirmed = true, RoleNames = roleNames }, model.Password, (key, message) => ModelState.AddModelError(key, message)) as User;

        //    if (user != null)
        //    {
        //        return Ok(model.UserName);
        //    }
        //    else
        //    {
        //        return StatusCode(204);
        //    }
        //}

        //[HttpPost]
        //[ActionName("Post03")]
        //[EnableCors("MyPolicy")]
        //public async Task<IActionResult> Post03(ContentItem newContentItem, bool draft = false)
        //{
        //    var contentItem = await _contentManager.GetAsync(newContentItem.ContentItemId, VersionOptions.DraftRequired);

        //    if (contentItem == null)
        //    {
        //        return StatusCode(204);
        //    }
        //    else
        //    {
        //        if (!await _authorizationService.AuthorizeAsync(User, Permissions.EditOwnContent, contentItem))
        //        {
        //            return Unauthorized();
        //        }
        //    }

        //    if (contentItem != newContentItem)
        //    {
        //        //string json = contentItem.Content;
        //        dynamic jsonObj = newContentItem.Content;
        //        jsonObj["Influencer"]["ShareLink"]["Text"] = "650000";
        //        //string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        //        //File.WriteAllText("settings.json", output);

        //        contentItem.DisplayText = newContentItem.DisplayText;
        //        contentItem.ModifiedUtc = newContentItem.ModifiedUtc;
        //        contentItem.PublishedUtc = newContentItem.PublishedUtc;
        //        contentItem.CreatedUtc = newContentItem.CreatedUtc;
        //        contentItem.Owner = newContentItem.Owner;
        //        contentItem.Author = newContentItem.Author;

        //        contentItem.Apply(newContentItem);

        //        await _contentManager.UpdateAsync(contentItem);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!draft)
        //    {
        //        await _contentManager.PublishAsync(contentItem);
        //    }

        //    return Ok(contentItem);
        //}

        [HttpPost]
        [ActionName("Post03")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> Post03(UpdateInfluencerModel influencerCostModel, bool draft = false)
        {
            var contentItem = await _contentManager.GetAsync(influencerCostModel.ContentItemId, VersionOptions.DraftRequired);

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

            dynamic jsonObj = contentItem.Content;
            jsonObj["Influencer"]["ShareLink"]["Text"] = influencerCostModel.ShareLinkCost;
            jsonObj["Influencer"]["PostImage"]["Text"] = influencerCostModel.PostImageCost;
            jsonObj["Influencer"]["Video"]["Text"] = influencerCostModel.VideoCost;
            jsonObj["Influencer"]["CheckIn"]["Text"] = influencerCostModel.CheckinCost;
            jsonObj["Influencer"]["LiveStream"]["Text"] = influencerCostModel.LiveStreamCost;

            contentItem.ModifiedUtc = DateTime.Now;

            await _contentManager.UpdateAsync(contentItem);


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
        [ActionName("UpdateFollowerAndPhoto")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> UpdateFollowerAndPhoto(UpdateFollowerAndPhotoModel followerAndPhotoModel, bool draft = false)
        {
            var contentItem = await _contentManager.GetAsync(followerAndPhotoModel.ContentItemId, VersionOptions.DraftRequired);

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

            dynamic jsonObj = contentItem.Content;
            jsonObj["Influencer"]["NumberOfFollowers"]["Value"] = followerAndPhotoModel.NumberOfFollowers;

            var photos = jsonObj["Influencer"]["Photo"]["Paths"];

            foreach (var item in followerAndPhotoModel.PhotoPaths)
            {
                photos.Add(item);
            }

            //jsonObj["Influencer"]["Photo"]["Paths"] = followerAndPhotoModel.PhotoPaths;
            contentItem.ModifiedUtc = DateTime.Now;

            await _contentManager.UpdateAsync(contentItem);


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
        [ActionName("UpdatePosts")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> UpdatePosts(UpdatePostModel updatePostModel, bool draft = false)
        {
            var contentItem = await _contentManager.GetAsync(updatePostModel.ContentItemId, VersionOptions.DraftRequired);

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

            dynamic jsonObj = contentItem.Content;
            jsonObj["Influencer"]["NumberOfPost"]["Value"] = updatePostModel.NumberOfPost;
            jsonObj["Influencer"]["NumberOfShare"]["Text"] = updatePostModel.NumberOfTotalShare;
            jsonObj["Influencer"]["NumberOfReaction"]["Text"] = updatePostModel.NumberOfTotalReaction;
            jsonObj["Influencer"]["NumberOfComment"]["Text"] = updatePostModel.NumberOfTotalComment;

            var indx = 0;

            foreach (var item in updatePostModel.Posts)
            {
                indx++;
                if (indx > updatePostModel.Posts.Count)
                {
                    break;
                }
                else
                {
                    jsonObj["Post" + indx.ToString()]["Link"]["Text"] = item.Link ?? "";
                    jsonObj["Post" + indx.ToString()]["NumberOfComment"]["Text"] = item.NumberOfComment;
                    jsonObj["Post" + indx.ToString()]["NumberOfReaction"]["Text"] = item.NumberOfReaction;
                    jsonObj["Post" + indx.ToString()]["NumberOfShare"]["Text"] = item.NumberOfShare;
                    jsonObj["Post" + indx.ToString()]["Status"]["Text"] = item.Status;
                    jsonObj["Post" + indx.ToString()]["Time"]["Text"] = item.Time;
                    jsonObj["Post" + indx.ToString()]["Title"]["Text"] = item.Title;
                    jsonObj["Post" + indx.ToString()]["Type"]["Text"] = item.Type;
                }

            }

            //jsonObj["Influencer"]["Photo"]["Paths"] = followerAndPhotoModel.PhotoPaths;
            contentItem.ModifiedUtc = DateTime.Now;

            await _contentManager.UpdateAsync(contentItem);


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

        [HttpGet]
        [ActionName("Post04")]
        [EnableCors("MyPolicy")]
        public IActionResult Post04()
        {
            if (User.HasClaim("Permission", "EditOwn_Brand"))
            {
                return Ok("Brand");
            }

            if (User.HasClaim("Permission", "EditOwn_Influencer"))
            {
                return Ok("Influencer");
            }

            return StatusCode(204);

        }
    }
}
