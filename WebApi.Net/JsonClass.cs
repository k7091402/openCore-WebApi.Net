using System.Text.Json;

public class Request
{
    public int request_id { get; set; }
    public string message { get; set; } = string.Empty;

    public Request() { }

    public Request(int rid, string msg)
    {
        request_id = rid;   
        message = msg;
    }

    public string RequestToJson()
    {
        var options = new JsonSerializerOptions 
        { 
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        string json = JsonSerializer.Serialize(this, options);

        return json;
    }
}