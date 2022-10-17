using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HostPort
{
    public static string host = "26.100.4.13";
    public static string port = "5000";
}


public class WebRequests : MonoBehaviour
{
    public AudioSource source;

    public GameObject Button1;

    void Start()
    {

    }

    void Update()
    {
        WebRequest request = WebRequest.Create("http://" + HostPort.host + ":" + HostPort.port + "/get_action?device=VR");
        WebResponse response = request.GetResponse();
        request.Method = "POST";
        var httpResponse = (HttpWebResponse)request.GetResponse();
        var result = "";
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }
        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        if (values["action"] == "VineBoom")
        {
            source.Play();
            Debug.Log(":TheRock:");
        }

        if (values["action"] == "Button1")
        {
            if (Button1.activeSelf)
            {
                Button1.SetActive(false);
            }
            else
            {
                Button1.SetActive(true);
            }

        }

    }
 
}
