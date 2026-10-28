using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Extensions.Images
{
    public static class Functions
    {
        public static byte[] ToByteArray(this Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using var ms = new MemoryStream();
            image.Save(ms, format);
            return ms.ToArray();
        }
    }
}
