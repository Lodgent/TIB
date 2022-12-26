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
    public static string code = "";
}


public class WebRequests : MonoBehaviour
{
    public AudioSource SpawnObject;
    public AudioSource MovePlatform;
    public GameObject EscapeButton;
    public GameObject BlueButton;
    public GameObject GreenButton;
    public GameObject CeilButton;
    private bool IsGameStarted;
    public GameObject door;
    public AudioSource DoorOpen;
    public AudioSource DoorOpenMechanic;


    void Start()
    {
        MovePlatform.volume = 0.5f;
        IsGameStarted = false;
    }

    void spawn_object(GameObject obj, string[] commands, float deltaX, float deltaY, float deltaZ)
    {
        var coords = commands[1].Split(',');
        var x = float.Parse(coords[0]);
        var y = float.Parse(coords[1]);
        var z = float.Parse(coords[2]);
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
        WebRequest request = WebRequest.Create("http://" + HostPort.host + ":" + HostPort.port + "/get_action?device=VR&code=" + HostPort.code);
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
            spawn_object(EscapeButton, commands, 1f, 0f, 1f);
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
        else if (commands[0] == "MovePlatformUpStart")
        {
            MoveObject.moveUp = true;
            MovePlatform.Play();
        }
        else if (commands[0] == "MovePlatformUpEnd")
        {
            MoveObject.moveUp = false;
            MovePlatform.Stop();
        }
        else if (commands[0] == "MovePlatformDownStart")
        {
            MoveObject.moveDown = true;
            MovePlatform.Play();
        }
        else if (commands[0] == "MovePlatformDownEnd")
        {
            MoveObject.moveDown = false;
            MovePlatform.Stop();
        }
        else if (commands[0] == "blue_button")
        {
            spawn_object(BlueButton, commands, 0f, 0.25f, 42f);
        }
        else if (commands[0] == "green_button")
        {
            spawn_object(GreenButton, commands, 0f, 0.25f, 42f);
           
        }
        else if (commands[0] == "ceil_button1")
        {
            spawn_object(CeilButton, commands, 0f, 0f, 0f);
        }
        else if (commands[0] == "ceil_button2")
        {
            spawn_object(CeilButton, commands, 0f, 0f, 42f);
        }
        else if (commands[0] == "ceil_button3")
        {
            spawn_object(CeilButton, commands, -2f, -16f, 24f);
        }
        else if (commands[0] == "start_game")
        {
            if (!IsGameStarted)
            {
                IsGameStarted = true;
                StartCoroutine(start_game(door));
            }
        }
    }

    IEnumerator start_game(GameObject Door)
    {
        DoorOpen.Play();
        yield return new WaitForSeconds(2);
        DoorOpenMechanic.Play();
        for (int i = 0; i < 30; i++)
        {
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 0.06f, Door.transform.position.z);
            yield return new WaitForSeconds(0.03f);
        }

    }

    public GameObject GreenGet()
    {
        return GreenButton;
    }

}
