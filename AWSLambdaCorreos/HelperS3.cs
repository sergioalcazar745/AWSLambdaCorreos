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
        //EL NOMBRE DEL BUCKET LO INCLUIREMOS DENTRO DE 
        //APPSETTINGS.JSON
        private string BucketName;
        //S3 TRABAJA CON UNA INTERFACE LLAMADA IAmazonS3
        private IAmazonS3 ClientS3;
        public HelperS3(IAmazonS3 ClientS3, string bucketName)
        {
            this.ClientS3 = ClientS3;
            this.BucketName = bucketName;
        }

        //COMENZAMOS SUBIENDO FICHEROS AL BUCKET
        //NECESITAMOS FileName, Stream y un Key/Value
        public async Task<bool>
            UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.BucketName
            };
            //DEBEMOS OBTENER UNA RESPUESTA CON EL MISMO TIPO 
            //DE REQUEST
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

        //ELIMINAMOS FICHEROS DEL BUCKET
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

        //METODO PARA RECUPERAR LAS VERSIONES DE UN FILE
        public async Task<List<string>> GetVersionsFilesAsync()
        {
            ListVersionsResponse response =
                await this.ClientS3.ListVersionsAsync(this.BucketName);
            List<string> versiones =
                response.Versions.Select(x => x.Key).ToList();
            return versiones;
        }

        //METODO PARA RECUPERAR UN FILE POR CODIGO
        public async Task<Stream> GetFileAsync(string fileName)
        {
            GetObjectResponse response =
                await this.ClientS3.GetObjectAsync(this.BucketName, fileName);
            string url = response.ETag;

            return response.ResponseStream;
        }
    }

}
