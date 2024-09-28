using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Invitro.Models;

public class AmChat
{
    static string questionTemplate = @"{
        ""model"": ""gpt-3.5-turbo"",
        ""messages"": [
            {
                ""role"": ""system"",
                ""content"": ""You will receive message with 
mount of data of appointments, you need to find patterns of dates or time, 
and you need to summarize this info into a helpful advice or helpful message
for manager. Main aim is to spread appointments. So say about most loaded date or time.""
            },
            {
                ""role"": ""user"",
                ""content"": ""{0}""
            }
        ]
    }";

    public async Task<string> GetAnswer(string question)
    {
        //question = CleanseString(question);
        string jsonContent = questionTemplate.Replace("{0}", question);

        var httpClient = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
            Headers =
            {
                { HttpRequestHeader.ContentType.ToString(), "application/json" },
                {
                    HttpRequestHeader.Authorization.ToString(),
                    "Bearer " +
                    "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"
                },
            },
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };

        var response = await httpClient.SendAsync(request);
        string answer = await GetClearAnswerFromResponse(response);

        // Сохранение в DB, по итогу имеем question и answer
        return answer;

        static async Task<string> GetClearAnswerFromResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var parsedResponse = JObject.Parse(responseContent);

                var choices = parsedResponse["choices"];
                if (choices == null) return "*** 1";
                if (choices.Count() == 0) return "*** 2";
                if (choices[0] == null) return "*** 3";
                var message = choices![0]!["message"];
                if (message == null) return "*** 4";
                if (message["content"] == null) return "*** 5";
                var answer = message["content"]!.ToString();

                return answer;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return $"Error: {response.StatusCode}, Content: {errorContent}";
        }

        static string CleanseString(string input)
        {
            return JsonConvert.ToString(input);
        }

    }
}