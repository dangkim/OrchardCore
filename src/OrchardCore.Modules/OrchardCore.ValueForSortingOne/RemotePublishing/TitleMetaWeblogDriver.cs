using System.Text.Encodings.Web;
using OrchardCore.ContentManagement;
using OrchardCore.XmlRpc;
using OrchardCore.XmlRpc.Models;
using OrchardCore.MetaWeblog;
using OrchardCore.Title.Model;

namespace OrchardCore.Title.RemotePublishing
{
    public class TitleMetaWeblogDriver : MetaWeblogDriver
    {
        private readonly HtmlEncoder _encoder;

        public TitleMetaWeblogDriver(HtmlEncoder encoder)
        {
            _encoder = encoder;
        }

        public override void BuildPost(XRpcStruct rpcStruct, XmlRpcContext context, ContentItem contentItem)
        {
            var titlePart = contentItem.As<ValueForSearchingOnePart>();
            if (titlePart == null)
            {
                return;
            }

            rpcStruct.Set("title", _encoder.Encode(titlePart.Title));
        }

        public override void EditPost(XRpcStruct rpcStruct, ContentItem contentItem)
        {
            contentItem.DisplayText = rpcStruct.Optional<string>("title");
        }
    }
}
