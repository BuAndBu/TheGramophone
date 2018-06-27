using System;
using System.Collections.Generic;
using System.Text;
using Amazon.S3;
using Amazon;
using Amazon.Runtime;
using Amazon.S3.Model;
using System.Threading.Tasks;
using System.IO;

namespace TheGramophone.Services
{
    class StoriesS3Service : S3Service
    {
        public StoriesS3Service(string accessKey, string secretKey, string bucketName) : base(accessKey, secretKey)
        {
            this.bucketName = bucketName;
        }

        private string bucketName { get; set; }

        public async Task<List<S3Object>> ListStories()
            => (await AWSS3Client.ListObjectsAsync(bucketName)).S3Objects;

        public string GetStoryUrl(string storyName)
            => AWSS3Client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = storyName,
                    Expires = DateTime.Now.AddDays(1),
                    Protocol = Protocol.HTTPS
                });
        
        public string GetRandomStoryName(List<S3Object> stories)
            => stories[(new Random()).Next(0, stories.Count)].Key;
    }
}
