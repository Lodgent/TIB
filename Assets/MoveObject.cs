using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    public static bool moveLeft = false;
    public static bool moveRight = false;

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
        if (moveLeft)
        {
            move(platform, 0f, 0f, -0.01f);
        }

        if (moveRight)
        {
            move(platform, 0f, 0f, 0.01f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("triggeer");
            while (moveLeft)
            {
                move(player, 0f, 0f, -0.01f);
            }

            while(moveRight)
            {
                move(player, 0f, 0f, 0.01f);
            }
        }
    }
}
