using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.BitmapManager
{
    public interface IImageManager
    {
        Stream GlueImages(Stream firstImage, Stream secondImage);
    }
}
