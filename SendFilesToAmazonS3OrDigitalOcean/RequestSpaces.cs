using SendFilesToAmazonS3OrDigitalOcean.Model;
using SendFilesToAmazonS3OrDigitalOcean.SendPreparation;

namespace SendFilesToAmazonS3OrDigitalOcean
{
    public class RequestSpaces
    {
        public static void Request(Config config, string Path)
        {
            if (config is not null)
            {
                IEnumerable<string> GetFilesFromDir(string dir) => Directory.EnumerateFiles(dir)
                    .Concat(Directory.EnumerateDirectories(dir)
                    .SelectMany(subdir => GetFilesFromDir(subdir)));

                IEnumerable<string> listaDeArquivos = GetFilesFromDir(Path);

                Parallel.ForEach(listaDeArquivos,
                    new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                    arquivo =>
                    {
                        Send.POST(config, arquivo);
                    });

                Console.WriteLine("Fim");
            }
        }

        public static async void Deleted(Config config)
        {
            if (config is not null)
                await Send.DeleteFolder(config);
        }
    }
}
