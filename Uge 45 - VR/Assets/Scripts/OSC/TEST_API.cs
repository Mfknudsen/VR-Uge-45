using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TEST_API : MonoBehaviour
{
    //https://github.com/marcteys/unity-hue

    void Start()
    {
        StartCoroutine(TestConnection());
    }

    //Det ser ud til at man skal bruge ienumertors til at sikre resten af koden først kører når man har fået et kald tilbage.
    IEnumerator TestConnection()
    {
        UnityWebRequest w = UnityWebRequest.Get("http://w.'servername'.com");

        yield return w.SendWebRequest();

        if (w.isNetworkError || w.isHttpError)
        {
            Debug.Log(w.error);
        }
        else
        {
            Debug.Log(w.downloadHandler.data);

            byte[] results = w.downloadHandler.data;
        }

        StartCoroutine(UploadNewDATA());
    }

    IEnumerator UploadNewDATA()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        UnityWebRequest www = UnityWebRequest.Put("http://www.'servername'/upload", myData);
        //OR
        string newData = "This sting will be uploaded to the API";
        UnityWebRequest w = UnityWebRequest.Put("http://www.'servername'/upload", newData);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }
}
