using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequence : MonoBehaviour
{
    // Start is called before the first frame update
    private string Solution;
    public char Number;
    public Light light;
    public AudioSource source;
    public AudioSource bad;
   
    void Start()
    {
        Solution = "256134";
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        
        if (State.Trigger)
            return;
        if (Solution[State.Now] == Number)
        {
            State.Now++;
            if (State.Now > 5)
            {
                light.color = Color.green;
                GiveCommand.StaticPostRequest("GiveCeilButton");
                State.Trigger = true;
                source.Play();

            }
        }
        else
        {
            bad.Play();
            State.Now = 0;
        }
    }
}
