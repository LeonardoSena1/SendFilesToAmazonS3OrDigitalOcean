using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.S3;
using Amazon.S3.Model;
using SendFilesToAmazonS3OrDigitalOcean.Model;

namespace SendFilesToAmazonS3OrDigitalOcean.SendPreparation
{
    public class Send
    {
        /// <summary>
        /// Send all the objects Amazon S3 bukcet
        /// </summary>
        /// <param name="file"></param>
        public static void POST(Config config, string File)
        {
            try
            {
                AmazonS3Config ClientConfig = new AmazonS3Config();
                ClientConfig.ServiceURL = config.Url;
                IAmazonS3 s3Client = new AmazonS3Client(config.User, config.Pass, ClientConfig);
                FileInfo filee = new FileInfo(File);
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = filee.OpenRead(),
                    BucketName = config.BucketName,
                    Key = config.Key,
                    CannedACL = S3CannedACL.PublicRead
                };
                s3Client.PutObjectAsync(request).Wait();
                Console.WriteLine("Arquivo Salvo: " + filee.Name);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        /// <summary>
        /// Delete all of the objects stored in an existing Amazon S3 bucket.
        /// contents will be deleted.</param>
        /// deleting all of the objects in the bucket.</returns>
        /// </summary>
        public static async Task DeleteFolder(Config config)
        {
            try
            {
                ListObjectsV2Response response;
                AmazonS3Config S3Config = new AmazonS3Config();
                S3Config.ServiceURL = config.Url;
                using (var s3Client = new AmazonS3Client(config.User, config.Pass, S3Config))
                {
                    ListObjectsV2Request request =
                        new ListObjectsV2Request
                        {
                            BucketName = config.BucketName,
                            Prefix = config.Prefix
                        };

                    do
                    {
                        response = s3Client.ListObjectsV2Async(request).Result;
                        response.S3Objects
                           .ForEach(async obj => await s3Client.DeleteObjectAsync(config.BucketName, obj.Key));

                        request.ContinuationToken = response.ContinuationToken;
                    } while (response.IsTruncated);
                }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error deleting objects: {ex.Message}");
            }
        }
    }
}
