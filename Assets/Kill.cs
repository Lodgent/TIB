using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float x;
    public float y;
    public float z;
    public AudioSource source;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Valve.VR.SteamVR_Fade.Start(Color.black, 0.25f);
            player = GameObject.Find("Player");
            source.Play();
            StartCoroutine(waiter());
            
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(x, y, z);
        yield return new WaitForSeconds(0.5f);
        Valve.VR.SteamVR_Fade.Start(Color.clear, 0.5f);
        //set and start fade to



    }


}
