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
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (State.SpeedBrain == 1 && !start)
        {
            pressedTime = DateTime.Now;
            start = true;
        }
        
        if (State.SpeedBrain == 2)
        {
            if (int.Parse((pressedTime - DateTime.Now).ToString("ss")) < 5)
            {
                source.Play();
                light.color = Color.green;
                State.SpeedBrain = 0;
                start = false;
            }
        }

        if (start)
        {
            if (int.Parse((pressedTime - DateTime.Now).ToString("ss")) > 5)
            {
                State.SpeedBrain = 0;
                State.LastSpeedBrain = "";
                Debug.Log("WRONG!");
                start = false;
            }
        }
    }
}
