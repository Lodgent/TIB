using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/action?action=" + action + "&device=SITE";
        UnityWebRequest request = UnityWebRequest.Post(requestUrl, "");
        request.SendWebRequest();

    }


}
