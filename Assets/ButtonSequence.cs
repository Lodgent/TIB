using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequence : MonoBehaviour
{
    // Start is called before the first frame update
    private string Solution;
    public char Number;
   
    void Start()
    {
        Solution = "216354";
       
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
                Debug.Log("You found it!");
                State.Trigger = true;
            }
        }
        else
        {
            State.Now = 0;
        }
    }
}
