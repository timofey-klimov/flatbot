using System.IO;

namespace Utils
{
    public static class ConvertExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            if (stream == null)
                return default;

            if (stream is MemoryStream ms)
            {
                return ms.ToArray();
            }

            var buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);

            return buffer;
        }
    }
}
