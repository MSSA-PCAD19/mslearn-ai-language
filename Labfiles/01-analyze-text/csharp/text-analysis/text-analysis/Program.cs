using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string endpoint = config["endpoint"];
string key = config["key"];
string fileName = @"reviews\review4.txt";

Console.WriteLine($"======Language Detection=======");
var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

Response<DetectedLanguage> result = client.DetectLanguage(File.ReadAllText(fileName));
Console.WriteLine($"Language detected is : {result.Value.Name}, confidence: {result.Value.ConfidenceScore}");

Console.WriteLine($"======Sentiment================");
Response<DocumentSentiment> resultSentiment= client.AnalyzeSentiment(File.ReadAllText(fileName));
Console.WriteLine($"Sentiment is : {resultSentiment.Value.Sentiment}");
Console.WriteLine($"""
 Sentiment Confidence
    negative:{resultSentiment.Value.ConfidenceScores.Negative}
    neutral:{resultSentiment.Value.ConfidenceScores.Neutral}
    positive:{resultSentiment.Value.ConfidenceScores.Positive}
 """);

Console.WriteLine($"======Key Phrases=======");
var responseKeyPhrases = client.ExtractKeyPhrases(File.ReadAllText(fileName));
foreach (var item in responseKeyPhrases.Value)
{
    Console.WriteLine(item);
}

Console.WriteLine($"======Recognize Entities=======");
var responseEntity = client.RecognizeEntities(File.ReadAllText(fileName));
foreach (var item in responseEntity.Value)
{
    Console.WriteLine($"{item.Category}:{item.Text}");
}

