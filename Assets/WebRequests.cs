using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class WebRequests : MonoBehaviour
{
    public AudioSource source;

    void Start()
    {

    }

    void Update()
    {
        WebRequest request = WebRequest.Create("http://26.100.4.13:5000/get_action?device=VR");
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

    }
 
}
