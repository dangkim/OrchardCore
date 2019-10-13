using System;
using System.Collections.Generic;
using System.Text;

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

    public class UpdateFollowerAndPhotoModel
    {
        public string ContentItemId { get; set; }
        public List<string> PhotoPaths { get; set; }
        public int NumberOfFollowers { get; set; }
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
    }
}
