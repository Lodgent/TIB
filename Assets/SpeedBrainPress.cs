using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBrainPress : MonoBehaviour
{
    public string Button;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {

        if (Button == "Green" && State.LastSpeedBrain != "Green")
        {
            State.LastSpeedBrain = "Green";
            State.SpeedBrain++;

        }
        else if (Button == "Blue" && State.LastSpeedBrain != "Blue")
        {
            State.LastSpeedBrain = "Blue";
            State.SpeedBrain++;
        }

    }
}
