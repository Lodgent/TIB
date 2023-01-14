using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;


public class GiveCommand : MonoBehaviour
{
    public string action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostRequest()
    {
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/action?action=" + action + "&device=SITE" + "&code=" + HostPort.code;
        UnityWebRequest request = UnityWebRequest.Post(requestUrl, "");
        request.SendWebRequest();

    }

    static public void StaticPostRequest(string ac)
    {
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/action?action=" + ac + "&device=SITE";
        UnityWebRequest request = UnityWebRequest.Post(requestUrl, "");
        request.SendWebRequest();
    }

}
