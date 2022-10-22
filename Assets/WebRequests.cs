using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class HostPort
{
    public static string host = "26.100.4.13";
    public static string port = "5000";
}


public class WebRequests : MonoBehaviour
{
    public AudioSource SpawnObject;

    public GameObject EscapeButton;

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
        var commands = values["action"].Split(':');
        if (commands[0] == "SpawnEscapeButton")
        {
            var coords = commands[1].Split(',');
            var x = float.Parse(coords[0]);
            var y = float.Parse(coords[1]);
            var z = float.Parse(coords[2]);
            EscapeButton.transform.position = new Vector3(x, y, z);
            SpawnObject.Play();
            if (y == 0f)
            {
                EscapeButton.transform.rotation = Quaternion.Euler(0, 0, 0);
                EscapeButton.transform.position = new Vector3(EscapeButton.transform.position.x,
                    EscapeButton.transform.position.y + 0.5f, EscapeButton.transform.position.z);
            }
            else if (y == 10f)
            {
                EscapeButton.transform.rotation = Quaternion.Euler(180, 0, 0);
                EscapeButton.transform.position = new Vector3(EscapeButton.transform.position.x,
                    EscapeButton.transform.position.y - 0.5f, EscapeButton.transform.position.z);
            }
            EscapeButton.SetActive(true);
        }
    }
}
