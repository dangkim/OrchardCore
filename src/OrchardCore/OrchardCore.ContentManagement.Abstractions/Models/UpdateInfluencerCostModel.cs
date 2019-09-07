using System;
using System.Collections.Generic;
using System.Text;

namespace OrchardCore.ContentManagement.Models
{
    public class UpdateInfluencerCostModel
    {
        public string ContentItemId { get; set; }
        public string ShareLinkCost { get; set; }
        public string VideoCost { get; set; }
        public string PostImageCost { get; set; }
        public string CheckinCost { get; set; }
        public string LiveStreamCost { get; set; }
    }
}
