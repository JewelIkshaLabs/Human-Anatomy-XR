using System.IO;
using System.Net;
using UnityEngine;

public static class APIHandler
{
    public static DataStorage GetRequest()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<DataStorage>(json);
    }
}
