using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET.Request;
using Alexa.NET;
using TheGramophone.Services;
using Amazon.S3.Model;
using Alexa.NET.Response.Directive;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace TheGramophone
{
    public class Function
    {
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            string accessKey = Environment.GetEnvironmentVariable("ACCESS_KEY");
            string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
            string bucketStories = Environment.GetEnvironmentVariable("BUCKET_STORIES");

            StoriesS3Service storiesService = new StoriesS3Service(accessKey, secretKey, bucketStories);

            string audioUrl;
            string audioToken;
            SsmlOutputSpeech ssmlResponse = new SsmlOutputSpeech();

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                ssmlResponse.Ssml = "<speak>You can't open this skill.</speak>";
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                IntentRequest intentRequest = (IntentRequest)input.Request;
                switch (intentRequest.Intent.Name)
                {
                    case "TellRandomStory":
                        string randomStoryName = storiesService.GetRandomStoryName(await storiesService.ListStories());
                        audioUrl = storiesService.GetStoryUrl(randomStoryName);
                        audioToken = "A story.";
                        return ResponseBuilder.AudioPlayerPlay(PlayBehavior.ReplaceAll, audioUrl, audioToken);
                    default:
                        ssmlResponse.Ssml = $"<speak>An error has occurred!</speak>";
                        break;
                }

            }
            return ResponseBuilder.Tell(ssmlResponse);
        }
    }
}
