using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Vector3 = UnityEngine.Vector3;


public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    private CharacterController characterController;
    public Transform playercent;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 direction =  Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
       characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0,9.81f, 0) * Time.deltaTime);
       characterController.center = new Vector3(playercent.localPosition.x, 1.03f, playercent.localPosition.z);
    }
}
