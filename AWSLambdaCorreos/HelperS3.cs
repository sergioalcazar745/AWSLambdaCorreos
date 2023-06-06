using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaCorreos
{
    public class HelperS3
    {
        private string BucketName;
        private IAmazonS3 ClientS3;
        public HelperS3(IAmazonS3 ClientS3, string bucketName)
        {
            this.ClientS3 = ClientS3;
            this.BucketName = bucketName;
        }

        public async Task<bool>
            UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.BucketName
            };

            PutObjectResponse response = await
                this.ClientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            DeleteObjectResponse response =
                await this.ClientS3.DeleteObjectAsync(this.BucketName, fileName);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<string>> GetVersionsFilesAsync()
        {
            ListVersionsResponse response =
                await this.ClientS3.ListVersionsAsync(this.BucketName);
            List<string> versiones =
                response.Versions.Select(x => x.Key).ToList();
            return versiones;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            GetObjectResponse response =
                await this.ClientS3.GetObjectAsync(this.BucketName, fileName);
            string url = response.ETag;

            return response.ResponseStream;
        }
    }

}
