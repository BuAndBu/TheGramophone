using System;
using Amazon.S3;
using Amazon.Runtime;

namespace TheGramophone.Services
{
    abstract class S3Service : IS3Service
    {
        public S3Service(string accessKey, string secretKey)
        {
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            AWSS3Client = new AmazonS3Client(new BasicAWSCredentials(accessKey, secretKey), Amazon.RegionEndpoint.USEast1);
        }

        private string accessKey { get; set; }

        private string secretKey { get; set; }

        protected IAmazonS3 AWSS3Client { get; set; }
    }
}
