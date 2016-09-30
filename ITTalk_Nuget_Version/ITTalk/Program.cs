using GetImages;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITTalk
{

    public class Image
    {
        public string Byte64Image { get; set; }
        public Image(byte[] byte64Image)
        {
            Byte64Image = Convert.ToBase64String(byte64Image);
        }
    }

    public class Program
    {
        private const string serverUrl = "http://192.168.1.6:5000";
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No parameters");
                return;
            }
            var id = Int16.Parse(args[0]);
            var image = new Image(ReadImage(id).Result);
            SendImage(image, id);
        }

        private static async Task<byte[]> ReadImage(int imageNo)
        {
            return await Downloader.Download(imageNo);
        }

        private static void SendImage(Image image, int id)
        {
            Console.WriteLine("Preparing connection");
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    var content = new StringContent('"' + image.Byte64Image + '"', Encoding.UTF8, "application/json");

                    var template = $@"<!DOCTYPE html><html><body><img id='base64image' src='data:image/jpeg;base64, {image.Byte64Image}' /></body></html>";
                    System.IO.File.WriteAllText("image.html", template);

                    var result = client.PostAsync($"/api/register/{id}", content).Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                }

                Console.WriteLine("Image has been sent");
            }
            catch (System.AggregateException)
            {
                Console.WriteLine($"Can't connect to the server ");
            }
        }
    }
}
