using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace OrchardCore.ContentManagement.Models
{
    public class UpdateInfluencerModel
    {
        public string ContentItemId { get; set; }
        public string ShareLinkCost { get; set; }
        public string VideoCost { get; set; }
        public string PostImageCost { get; set; }
        public string CheckinCost { get; set; }
        public string LiveStreamCost { get; set; }
    }

    public class DeleteCampaignModel
    {
        public string ContentItemId { get; set; }
    }

    public class UpdateFollowerAndPhotoModel
    {
        public string ContentItemId { get; set; }
        public List<string> PhotoPaths { get; set; }
        public int NumberOfFollowers { get; set; }

        public string ChartDate { get; set; }
    }

    public class UpdateVideoModel
    {
        public string ContentItemId { get; set; }
        public List<string> VideoPaths { get; set; }
    }

    public class UpdatePostModel
    {
        public string ContentItemId { get; set; }
        public List<PostModel> Posts { get; set; }
        public string NumberOfTotalComment { get; set; }
        public string NumberOfTotalReaction { get; set; }
        public string NumberOfTotalShare { get; set; }
        public string NumberOfPost { get; set; }
        public int SortingValueOne { get; set; }
        public int SortingValueTwo { get; set; }
        public int SortingValueThree { get; set; }
    }

    public class UpdateCampaignModel
    {
        public string ContentItemId { get; set; }
        public int Budget { get; set; }
        public string CampaignName { get; set; }
        public string CampaignTarget { get; set; }
        public string FromAge { get; set; }
        public int ToAge { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string InfluencerFullName { get; set; }
    }

    public class UpdateCampaignStatusModel
    {
        public List<string> ContentItemIds { get; set; }
        public int Status { get; set; }
    }

    public class UpdateBrandModel
    {
        public string ContentItemId { get; set; }
        public string FullName { get; set; }
        public string BrandName { get; set; }
        public string BusinessAreas { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        public int SortingValueOne { get; set; }
        public int SortingValueTwo { get; set; }
        public int SortingValueThree { get; set; }
    }

    public class PostModel
    {
        public string Link { get; set; }
        public string NumberOfComment { get; set; }
        public string NumberOfReaction { get; set; }
        public string NumberOfShare { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int SortingValueOne { get; set; }
        public int SortingValueTwo { get; set; }
        public int SortingValueThree { get; set; }
    }

    public class UpdateDisplayModel
    {
        public string ContentItemId { get; set; }
        public string DisplayText { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public int SortingValueOne { get; set; }
        public int SortingValueTwo { get; set; }
        public int SortingValueThree { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ContentItemId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }

    public class UploadAvatarModel
    {
        public string ContentItemId { get; set; }
        public ICollection<IFormFile> Files { get; set; }
    }
}
