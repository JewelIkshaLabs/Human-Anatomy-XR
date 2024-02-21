using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using System;
using System.Text;

[Serializable]
public class MyData 
{
    public string speech;
    public string text;
}

public class VoiceInput : MonoBehaviour
{
    public List<string> microphoneNames = new();
    public AudioSource audioSource;
    public AudioClip audioFile;
    public static string highlightString;
    public static string parts;
    public GameObject micButton;
    AudioClip microphoneClip;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var device in Microphone.devices)
        {
            microphoneNames.Add(device);
        }
        Debug.Log(Directory.GetFiles(Application.persistentDataPath, "*")[0]);
        // SaveWav.Save("converted", audioFile);
        // StartCoroutine(PostRequest(Directory.GetFiles(Application.persistentDataPath, "*")[0]));
        // Record();
        // StartCoroutine(Play());  
    }

    void Update()
    {
        if(Microphone.IsRecording(microphoneNames[0]))
        {
            micButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            micButton.GetComponent<Image>().color = Color.white;
        }   
    }

    public void InvokeMicAction()
    {
        bool isRecording = Microphone.IsRecording(microphoneNames[0]);

        if(isRecording)
        {
            StopRecording(); 
        } 
        else
        {
            Record();
        }
    }

    void Record()
    {
        microphoneClip = Microphone.Start(microphoneNames[0], false, 10, AudioSettings.outputSampleRate);
        Debug.Log("Recording...");
        StartCoroutine(WaitForRecording()); 
    }

    void StopRecording()
    {
        Microphone.End(microphoneNames[0]);
        Debug.Log("Recording Stopped...");
        CallAPI();
    }

    IEnumerator WaitForRecording()
    {
        yield return new WaitForEndOfFrame();

        while(Microphone.IsRecording(microphoneNames[0])) yield return null;
    }

    void CallAPI()
    {
        SavWav.Save("converted.wav",microphoneClip);
        StartCoroutine(PostRequest(Directory.GetFiles(Application.persistentDataPath, "*")[0],""));
        // StartCoroutine(PostRequest(audio_data,""));
    }

    private byte[] ConvertAndWrite (AudioClip clip, int bitRate)
    {
    
        float[] samples = new float[clip.samples];
    
        clip.GetData (samples, 0);
    
        Debug.Log(samples.Length);
    
        Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]
    
        Byte[] bytesData = new Byte[samples.Length * 2];
        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.
    
        float rescaleFactor = 32767; //to convert float to Int16
    
        for (int i = 0; i < samples.Length; i++) {
            intData[i] = (short)(samples[i] * rescaleFactor);
            Byte[] byteArr = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        return bytesData;
    }

    public IEnumerator PostRequest(string path, string question)
    {
        PartDetails.Instance.loadingIcon.SetActive(true);
        string url = "http://localhost:8080/listen1";

        WWWForm form = new();

        // Add the file data to the form
        if(path != "") 
        {
            byte[] fileData = File.ReadAllBytes(path);
            form.AddBinaryData("audio", fileData, "audio.wav", "audio/mp3");
        }
        else if(question != "") form.AddField("question", question);
        form.AddField("Mp3Path", Application.persistentDataPath);
        form.AddField("parts", parts);
        
        // Create a UnityWebRequest and send the form
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            // AudioClip audioClip = WavUtility.ToAudioClip($"{Application.persistentDataPath}/response.wav");
            try{
                string jsonText = www.downloadHandler.text;
                MyData data = JsonUtility.FromJson<MyData>(jsonText);
                byte[] receivedByteArray = Convert.FromBase64String(data.speech);
                AudioClip audioClip = WavUtility.ToAudioClip(receivedByteArray, 0, "wav");
                audioSource.PlayOneShot(audioClip);
                StartCoroutine(WriteText(data.text));
                highlightString = data.text;
            }
            catch {}

        }
        else
        {
            Debug.LogError(www.error);
        }
        PartDetails.Instance.loadingIcon.SetActive(false);
    }

    IEnumerator WriteText(string textData)
    {
        PartDetails.Instance.description.text = "";
        foreach(char letter in textData)
        {
            PartDetails.Instance.description.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
