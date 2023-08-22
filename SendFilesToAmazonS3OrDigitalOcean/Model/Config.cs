namespace SendFilesToAmazonS3OrDigitalOcean.Model
{
    public class Config
    {
        public string Url { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        public string Prefix { get; set; }
    }
}
