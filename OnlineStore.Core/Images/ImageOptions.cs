using Microsoft.Extensions.Configuration;

namespace OnlineStore.Core.Images
{
    public sealed class ImageOptions
    {
        [ConfigurationKeyName("ImagesUrl")]
        public string ImagesUrl { get; set; }
    }
}
