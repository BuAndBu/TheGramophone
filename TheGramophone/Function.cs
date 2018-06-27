using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET.Request;
using Alexa.NET;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace TheGramophone
{
    public class Function
    {
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SsmlOutputSpeech innerResponse = new SsmlOutputSpeech();

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                innerResponse.Ssml = "<speak>You can't open this skill.</speak>";
            }

            await Task.CompletedTask;

            return ResponseBuilder.Tell(innerResponse);
        }
    }
}
