// See https://aka.ms/new-console-template for more information
using Flurl.Http;

Console.WriteLine("Hello, World!");

while(true)
{
    try
    {
        Console.WriteLine("Please enter your user prompt.");
        var userPrompt = Console.ReadLine();
        var aiRequest = await "http://localhost:1234/v1/chat/completions"
        .WithHeader("Content-Type", "application/json")
        .PostJsonAsync(new
        {
            messages = new[] {
            new { role = "system", content = "you are a professional assistant"},
            new { role = "user", content = userPrompt}
            },
            temperature = ".7",
            max_tokens = "-1",
            stream = "false"
        });

        var result = await aiRequest.GetJsonAsync<AIMessage>();
        Console.WriteLine(result.choices[0].message.content);
    }
    catch (Exception ex)
    {
        Console.WriteLine("there was a problem please try again.");
    }

}


public class AIMessage
{
    public string id { get; set; }
    public string _object { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public Choice[] choices { get; set; }
    public Usage usage { get; set; }
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

public class Choice
{
    public int index { get; set; }
    public Message message { get; set; }
    public string finish_reason { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}

