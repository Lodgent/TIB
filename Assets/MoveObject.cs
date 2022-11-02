using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    private KeyCode z = KeyCode.Z;
    private KeyCode x = KeyCode.X;

    private void Start()
    {

    }


    private void move(GameObject objectToMove, float x, float y, float z)
    {
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x + x,
            objectToMove.transform.position.y + y, objectToMove.transform.position.z + z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(z))
        {
            move(platform, 0f, 0f, -0.05f);
        }

        if (Input.GetKeyDown(x))
        {
            move(platform, 0f, 0f, 0.05f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("triggeer");
            if (Input.GetKeyDown(z))
            {
                move(player, 0f, 0f, -0.05f);
            }

            if (Input.GetKeyDown(x))
            {
                move(player, 0f, 0f, 0.05f);
            }
        }
    }
}
