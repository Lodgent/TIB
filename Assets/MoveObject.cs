using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject platform;
    private GameObject player;
    public static bool moveLeft = false;
    public static bool moveRight = false;
    public static bool moveUp = false;
    public static bool moveDown = false;
    private bool IsPlayerOnPlatform;
    public float leftLimit;
    public float rightLimit;
    public float upLimit;
    public float downLimit;

    private void Start()
    {
        IsPlayerOnPlatform = false;
        player = GameObject.FindWithTag("Player");  
    }


    private void move(GameObject objectToMove, float x, float y, float z)
    {
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x + x,
            objectToMove.transform.position.y + y, objectToMove.transform.position.z + z);
    }

    // Update is called once per frame
    void Update()
    {
        if (platform.transform.position.z > leftLimit)
        {
            if (moveLeft)
            {
                if (IsPlayerOnPlatform)
                    move(player, 0f, 0f, -0.01f);
                move(platform, 0f, 0f, -0.01f);
                
            }
        }

        if (platform.transform.position.z < rightLimit)
        {
            if (moveRight)
            {
                if (IsPlayerOnPlatform)
                    move(player, 0f, 0f, 0.01f);
                move(platform, 0f, 0f, 0.01f);
                
            }
        }
        if (platform.transform.position.y < upLimit)
        {
            if (moveUp)
            {
                if (IsPlayerOnPlatform)
                    move(player, 0f, 0.01f, 0f);
                move(platform, 0f, 0.01f, 0f);
                
            }
        }
        if (platform.transform.position.y > downLimit)
        {
            if (moveDown)
            {
                move(platform, 0f, -0.01f, 0f);
                if (IsPlayerOnPlatform)
                    move(player, 0f, -0.01f, 0f);
                
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsPlayerOnPlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsPlayerOnPlatform = false;
        }
    }

}
