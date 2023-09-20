using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FileReturnLambda.Job;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var response = new APIGatewayProxyResponse();
        //request.QueryStringParameters.TryGetValue("documentId", out string documentId);

        //var pathToFile = "D:\\aws learning\\PDF Passing Test\\HiTim.pdf";
        //var currentDir = Directory.GetCurrentDirectory();
        //var fileExists = File.Exists(currentDir + "\\HiTim.pdf");

        var fileExists = true;
        LambdaLogger.Log($"The File Exists?: {fileExists}");

        //var pdfBlob = await File.ReadAllBytesAsync(currentDir + "\\HiTim.pdf");
        var pdfEncodedString = PdfEncoded.EncodedString;

        if (fileExists)
        {
            //var pdfBlob = File.ReadAllBytes(pathToFile);
            //response.Body = Convert.ToBase64String(pdfBlob);
            response.Body =  pdfEncodedString;
            response.StatusCode = 200;
            response.Headers = new Dictionary<string, string> { { "Content-Type", "application/pdf" } };
            response.IsBase64Encoded = true;
        }
        else
        {
            response.Body = "File Not Found on Lambda";
            response.StatusCode = 404;
        }
        return response;
    }
}
