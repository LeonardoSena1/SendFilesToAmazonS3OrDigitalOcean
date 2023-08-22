using SendFilesToAmazonS3OrDigitalOcean;
using SendFilesToAmazonS3OrDigitalOcean.Model;

RequestSpaces.Request(
    new Config()
    {
        BucketName = "BucketName",
        Key = "key",
        Pass = "password",
        Url = "Url",
        User = "user"
    }, "C:\\");

//RequestSpaces.Deleted(
//    new Config()
//    {
//        BucketName = "BucketName",
//        Key = "key",
//        Pass = "password",
//        Url = "Url",
//        User = "user",
//        Prefix = "prefix"
//    });