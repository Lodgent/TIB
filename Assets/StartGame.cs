using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public AudioSource lightsUp;
    public float x;
    public float y;
    public float z;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartofGame()
    {
        //Valve.VR.SteamVR_Fade.Start(Color.black, 0.5f);
        //SceneManager.LoadScene(LevelName);
        //Valve.VR.SteamVR_Fade.Start(Color.clear, 0.5f);
        //StartCoroutine(waiter());
        player.transform.position = new Vector3(x, y, z);
        Valve.VR.SteamVR_LoadLevel.Begin("Level1", true, 1f);
        player.transform.position = new Vector3(x, y, z);
        GiveCommand.StaticPostRequest("CompleteLevel0");
        lightsUp.Play();
        //StartCoroutine(waiter2());
    }
}
