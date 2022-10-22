using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Door;
    public AudioSource DoorOpen;
    public AudioSource DoorOpenMechanic;
    public bool isTriggered = false;
    public Light light;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenEscapeDoor()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(waiter());
        }

    }

    IEnumerator waiter()
    {
        DoorOpen.Play();
        light.color = Color.green;
        yield return new WaitForSeconds(2);
        DoorOpenMechanic.Play();
        for (int i = 0; i < 50; i++)
        {
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 0.038f, Door.transform.position.z);
            yield return new WaitForSeconds(0.03f);
        }
        
    }
}
