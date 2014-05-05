using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Turkcell.Updater.Utility
{
    internal static class StreamContentExtensions
    {
        public static async Task<string> ReadTextAsync(this StreamContent streamContent, Encoding encoding = null)
        {
            string result = string.Empty;
            if (encoding == null)
                encoding = Encoding.UTF8;
            if (streamContent != null)
            {
                using (var reader = new StreamReader(await streamContent.ReadAsStreamAsync(), encoding))
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            return result;
        }
    }
}