namespace ET;

public static class JsonHelper
{
    public static string ToJson<T>(this T? obj)
    {
        string json = System.Text.Json.JsonSerializer.Serialize<T?>(obj);
        return json;
    }

    public static T? FromJson<T>(string json)
    {
        T? deserialize = System.Text.Json.JsonSerializer.Deserialize<T>(json);
        return deserialize;
    }
}