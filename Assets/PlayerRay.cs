using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerRay : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    public GameObject EscapeButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
        Vector3 endPosition = transform.position + (transform.forward * 100f);
        RaycastHit hit;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        if (SteamVR_Actions._default.TouchpadLaserTrigger[SteamVR_Input_Sources.RightHand].state)
        {
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.gameObject);
                lineRenderer.SetPosition(1, hit.point);
                if (SteamVR_Actions._default.TouchPadLasterButtonA[SteamVR_Input_Sources.RightHand].stateUp)
                {
                    string command = hit.collider.gameObject.name;
                    if (hit.collider.gameObject.name == "Stem" || hit.collider.gameObject.name == "Push")
                    {
                        command = "GiveEscapeButton";
                        EscapeButton.SetActive(false);
                    }
                    
                }
                    
            }
            else
            {
                lineRenderer.SetPosition(1, endPosition);
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
