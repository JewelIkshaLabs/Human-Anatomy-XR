using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samples.Whisper;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class VoiceInput : MonoBehaviour
{
    public List<string> microphoneNames = new();
    public AudioSource audioSource;
    AudioClip microphoneClip;
    public AudioClip audioFile;
    private readonly string fileName = "output.wav";
    // Start is called before the first frame update
    void Start()
    {
        foreach(var device in Microphone.devices)
        {
            microphoneNames.Add(device);
        }
        // Debug.Log(Directory.GetFiles(Application.persistentDataPath, "*")[0]);
        StartCoroutine(PostRequest(Directory.GetFiles(Application.persistentDataPath, "*")[0]));
        // Record();
        // StartCoroutine(Play());
    }
    void Record()
    {
        microphoneClip = Microphone.Start(microphoneNames[0], false, 5, AudioSettings.outputSampleRate);
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(6);
        audioSource.PlayOneShot(microphoneClip);
        byte[] data = SaveWav.Save(fileName, microphoneClip);
        string dataString = data.Aggregate(new StringBuilder(), (sb, b)=>sb.AppendFormat("{0:x2} ", b), sb=>sb.AppendFormat("({0})", data.Length).ToString());
        Debug.Log(dataString);
    }

    IEnumerator PostRequest(string filePath)
    {
        string url = "http://localhost:8080/listen1";

        WWWForm form = new();

        // Load the file as a byte array
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        // Add the file data to the form
        form.AddBinaryData("audio", fileData, "audio.wav", "audio/mp3");

        // Create a UnityWebRequest and send the form
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
}
