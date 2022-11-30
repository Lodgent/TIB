using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using DateTime = System.DateTime;

public class SpeedBrain : MonoBehaviour
{
    // Start is called before the first frame update
    private DateTime pressedTime;
    private bool start;
    public Light light;
    public AudioSource source;
    public AudioSource winMusic;
    public AudioSource timer;
    private int cd;
    void Start()
    {
        start = false;
        cd = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (State.SpeedBrain == 1 && !start)
        {
            pressedTime = DateTime.Now;
            start = true;
            timer.Play();
        }
        
        if (State.SpeedBrain == 2)
        {
            if (int.Parse((pressedTime - DateTime.Now).ToString("ss")) < cd)
            {
                timer.Stop();
                source.Play();
                light.color = Color.green;
                State.SpeedBrain = 0;
                start = false;
                GiveCommand.StaticPostRequest("GiveLaser");
                
            }
        }

        if (start)
        {
            if (int.Parse((pressedTime - DateTime.Now).ToString("ss")) > cd)
            {
                State.SpeedBrain = 0;
                State.LastSpeedBrain = "";
                timer.Stop();
                winMusic.Play();
                start = false;
            }
        }
    }
}
