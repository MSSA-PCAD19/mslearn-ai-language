using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string endpoint = config["endpoint"];
string key = config["key"];

var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(key));

Response<DetectedLanguage> result = client.DetectLanguage(File.ReadAllText(@"reviews\review2.txt"));
Console.WriteLine($"Language detected is : {result.Value.Name}, confidence: {result.Value.ConfidenceScore}");