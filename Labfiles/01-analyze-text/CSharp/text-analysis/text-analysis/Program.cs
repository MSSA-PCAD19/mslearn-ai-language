using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string endpoint = config["endpoint"];
string key = config["key"];
string readFile = @"reviews\review1.txt";

var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

Response<DetectedLanguage> result = client.DetectLanguage(File.ReadAllText(readFile));
Console.WriteLine($"The language detected is : {result.Value.Name}, confidence: {result.Value.ConfidenceScore}");

Response<DocumentSentiment> result2 = client.AnalyzeSentiment(File.ReadAllText(readFile));
Console.WriteLine($"\nSentiment: {result2.Value.Sentiment}\n");

var keyPhrases = client.ExtractKeyPhrases(File.ReadAllText(readFile));

foreach (var item in keyPhrases.Value)
{
    Console.WriteLine($"Key Phase: {item}");
}