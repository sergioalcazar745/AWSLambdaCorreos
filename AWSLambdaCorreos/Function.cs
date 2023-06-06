using Amazon.Lambda.Core;
using Amazon.S3;
using ApiProyectoExtraSlice.Helpers;
using AWSLambdaCorreos.Models;
using Microsoft.Extensions.DependencyInjection;
using MVCApiExtraSlice.Helpers;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaCorreos;

public class Function
{
    //private static ServiceProvider provider { get; set; }

    //public Function()
    //{
        
    //}

    public async Task<string> FunctionHandler(ModelCorreoLamdba input, ILambdaContext context)
    {
        var provider = new ServiceCollection().AddAWSService<IAmazonS3>().BuildServiceProvider();
        ModelEmail modelemail = JsonConvert.DeserializeObject<ModelEmail>(await HelperSecretsKey.GetSecret("credentials-email"));
        HelperMail helperMail = new HelperMail(modelemail);
        //IAmazonS3 amazon = provider.GetService<IAmazonS3>();
        //HelperS3 helperS3 = new HelperS3(amazon, "archivos-temporal");
        //List<Stream> streamers = new List<Stream>();

        //if (input.Attachments == null)
        //{
        //    await helperMail.SendMailAsync(input.Email, input.Asunto, input.Body);
        //}
        //else
        //{
        //    foreach (string name in input.Attachments)
        //    {
        //        streamers.Add(await helperS3.GetFileAsync(input.Attachments[0]));
        //    }
        //    await helperMail.SendMailAsync(input.Email, input.Asunto, input.Asunto, streamers);
        //}
        await helperMail.SendMailAsync(input.Email, input.Asunto, input.Body);
        return "Vale";
    }
}
