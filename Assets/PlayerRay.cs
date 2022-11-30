using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;

public class PlayerRay : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    public GameObject EscapeButton;
    public AudioSource LaserOn;
    public AudioSource LaserSend;
    public GameObject GreenButton;
    public GameObject BlueButton;
    private bool laserOnCooldown;
    void Start()
    {
        LaserOn.volume = 0.5f;
        lineRenderer.material.color = Color.green;
        laserOnCooldown = true;
    }
    public void PostRequest(string action)
    {
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/action?action=" + action + "&device=SITE";
        UnityWebRequest request = UnityWebRequest.Post(requestUrl, "");
        request.SendWebRequest();

    }
    IEnumerator WaitForColor(LineRenderer line)
    {
        line.material.color = Color.red;
        laserOnCooldown = false;
        yield return new WaitForSeconds(2);
        laserOnCooldown = true;
        line.material.color = Color.green;
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
        if (SteamVR_Actions._default.TouchpadLaserTrigger[SteamVR_Input_Sources.RightHand].stateDown)
            LaserOn.Play();
        if (SteamVR_Actions._default.TouchpadLaserTrigger[SteamVR_Input_Sources.RightHand].state)
        {
            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(1, hit.point);
                if (SteamVR_Actions._default.TouchPadLasterButtonA[SteamVR_Input_Sources.RightHand].stateUp && laserOnCooldown)
                {
                    StartCoroutine(WaitForColor(lineRenderer));
                    string command = hit.collider.gameObject.name;
                    if (hit.collider.gameObject.name == "Stem" || hit.collider.gameObject.name == "Push")
                    {
                        command = "GiveEscapeButton";
                        EscapeButton.SetActive(false);
                    }

                    if (hit.collider.gameObject.name == "Water")
                    {
                        command = "Water";
                    }

                    if (hit.collider.gameObject.name == "GreenButtonStem" ||
                        hit.collider.gameObject.name == "GreenButtonPush")
                    {
                        if (BlueButton.activeSelf)
                        {
                            command = "GiveGreenButton";
                            GreenButton.SetActive(false);
                        }

                    }
                    if (hit.collider.gameObject.name == "BlueButtonStem" ||
                        hit.collider.gameObject.name == "BlueButtonPush")
                    {
                        if (GreenButton.activeSelf)
                        {
                            command = "GiveBlueButton";
                            BlueButton.SetActive(false);
                        }

                    }

                    if (hit.collider.gameObject.name == "Stem2" || hit.collider.gameObject.name == "Push2")
                    {
                        command = "SequenceButton";
                    }


                    LaserSend.Play();
                    PostRequest(command);
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
