using System.Collections.Generic;

namespace BL.Services.Interfaces
{
    public interface IS3Bucket
    {
        public string GetImageLink(string imageName);

        public IEnumerable<string> GetImagesLinks(IEnumerable<string> imageNames);
    }
}
