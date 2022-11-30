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
    public static string host = "25.32.13.14";
    public static string port = "5000";
}


public class WebRequests : MonoBehaviour
{
    public AudioSource SpawnObject;
    public AudioSource MovePlatform;
    public GameObject EscapeButton;
    public GameObject BlueButton;
    public GameObject GreenButton;


    void Start()
    {
        MovePlatform.volume = 0.5f;
    }

    void spawn_object(GameObject obj, string[] commands, float deltaX, float deltaY, float deltaZ)
    {
        var coords = commands[1].Split(',');
        var x = float.Parse(coords[0]) + 1;
        var y = float.Parse(coords[1]);
        var z = float.Parse(coords[2]) + 1;
        obj.transform.position = new Vector3(x, y, z);
        SpawnObject.Play();
        if (y == 0f)
        {
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
            obj.transform.position = new Vector3(obj.transform.position.x + deltaX,
                obj.transform.position.y + 0.5f + deltaY, obj.transform.position.z + deltaZ);
        }
        else if (y == 10f)
        {
            obj.transform.rotation = Quaternion.Euler(180, 0, 0);
            obj.transform.position = new Vector3(obj.transform.position.x + deltaX,
                obj.transform.position.y - 0.5f - deltaY, obj.transform.position.z + deltaZ);
        }
        obj.SetActive(true);

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
        if (commands[0] == "button")
        {
            spawn_object(EscapeButton, commands, 0f, 0f, 0f);
        }
        else if (commands[0] == "MovePlatformLeftStart")
        {
            MoveObject.moveLeft = true;
            MovePlatform.Play();

        }
        else if (commands[0] == "MovePlatformRightStart")
        {
            MoveObject.moveRight = true;
            MovePlatform.Play();
        }
        else if (commands[0] == "MovePlatformLeftEnd")
        {
            MoveObject.moveLeft = false;
            MovePlatform.Stop();

        }
        else if (commands[0] == "MovePlatformRightEnd")
        {
            MoveObject.moveRight = false;
            MovePlatform.Stop();
        }
        else if (commands[0] == "blue_button")
        {
            spawn_object(BlueButton, commands, 0f, 0.25f, 20f);
        }
        else if (commands[0] == "green_button")
        {
            Debug.Log("xd");
            spawn_object(GreenButton, commands, 0f, 0.25f, 20f);
           
        }
    }
}
