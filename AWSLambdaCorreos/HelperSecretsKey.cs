using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using ApiProyectoExtraSlice.Models;
using Newtonsoft.Json;

namespace ApiProyectoExtraSlice.Helpers
{
    public static class HelperSecretsKey
    {
        public static async Task<string> GetSecret(string secretName)
        {
            string region = "us-east-1";
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                return null;
            }

            return response.SecretString;
        }

        public static async Task<string> GetConnectionString(string databaseName, string cadena)
        {
            ModelSecretKeyDatabase model = JsonConvert.DeserializeObject<ModelSecretKeyDatabase>(cadena);
            return $"server={model.Host};port={model.Port};user id=adminsql;password=Admin123;database={databaseName}";
        }
    }
}
