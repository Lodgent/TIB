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
        UnityWebRequest request = UnityWebRequest.Post("http://26.100.4.13:5000/action?action=pidge&device=SITE", "");
        request.SendWebRequest();

    }


}
