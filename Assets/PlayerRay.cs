using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using TMPro;

public class PlayerRay : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer lineRenderer;
    public GameObject EscapeButton;
    public AudioSource LaserOn;
    public AudioSource LaserSend;
    private GameObject GreenButton;
    private GameObject BlueButton;
    private bool laserOnCooldown;
    private GameObject CeilButton;
    public string lastray;
    public GameObject StartButton;
    public GameObject ExitCanvas;
    public GameObject jackbox;
    public GameObject startCanvas;
    private bool startOnce;
    void Start()
    {
        LaserOn.volume = 0.5f;
        lineRenderer.material.color = Color.green;
        laserOnCooldown = true;
        lastray = "";
        startOnce = false;
    }
    public void PostRequest(string action)
    {
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/action?action=" + action + "&device=SITE&code="+HostPort.code;
        UnityWebRequest request = UnityWebRequest.Post(requestUrl, "");
        request.SendWebRequest();

    }

    public void CreateSession(string code)
    {
        var requestUrl = "http://" + HostPort.host + ":" + HostPort.port + "/create_session?code=" + code;
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

                //level 0!!!
                if (SceneManager.GetActiveScene().name == "Level0")
                {
                    if (hit.collider.gameObject.name == "Start")
                    {
                        var image = StartButton.GetComponent<Image>();
                        image.color = Color.gray;
                    }
                    else
                    {
                        var image = StartButton.GetComponent<Image>();
                        image.color = Color.black;
                    }

                    if (hit.collider.gameObject.name == "ExitCanvas")
                    {
                        var image = ExitCanvas.GetComponent<Image>();
                        image.color = Color.gray;
                    }
                    else
                    {
                        var image = ExitCanvas.GetComponent<Image>();
                        image.color = Color.black;
                    }
                }




                if (hit.collider.gameObject.name != lastray)
                {
                    SteamVR_Actions.default_Haptic[SteamVR_Input_Sources.RightHand].Execute(0, 0.2f, 5, 0.2f);
                    lastray = hit.collider.gameObject.name;
                }

               

                if (SteamVR_Actions._default.TouchPadLasterButtonA[SteamVR_Input_Sources.RightHand].stateUp && laserOnCooldown)
                {
                    StartCoroutine(WaitForColor(lineRenderer));
                    string command = hit.collider.gameObject.name;
                    if (hit.collider.gameObject.name == "Stem" || hit.collider.gameObject.name == "Push")
                    {
                        EscapeButton = GameObject.Find("StemAndPush");
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
                        GreenButton = GameObject.Find("GreenButton");
                        BlueButton = GameObject.Find("BlueButton");
                        if (BlueButton.transform.position.y < 700f)
                        {
                            command = "GiveGreenButton";
                            GreenButton.transform.position = new Vector3(0f, 1000f, 0f);
                        }

                    }
                    if (hit.collider.gameObject.name == "BlueButtonStem" ||
                        hit.collider.gameObject.name == "BlueButtonPush")
                    {
                        GreenButton = GameObject.Find("GreenButton");
                        BlueButton = GameObject.Find("BlueButton");
                        if (GreenButton.transform.position.y < 700f)
                        {
                            command = "GiveBlueButton";
                            BlueButton.transform.position = new Vector3(0f, 1000f, 0f);
                        }

                    }

                    if (hit.collider.gameObject.name == "Stem2" || hit.collider.gameObject.name == "Push2")
                    {
                        command = "SequenceButton";
                    }

                    if (hit.collider.gameObject.name == "StemCeil" || hit.collider.gameObject.name == "PushCeil")
                    {
                        CeilButton = GameObject.Find("CeilButton");
                        command = "GiveCeilButton";
                        CeilButton.transform.position = new Vector3(0f, 1000f, 0f);
                    }

                    if (hit.collider.gameObject.name == "Start" && !startOnce)
                    {
                        char a = (char) Random.Range('a', 'z');
                        char b = (char)Random.Range('a', 'z');
                        char c = (char)Random.Range('a', 'z');
                        char d = (char)Random.Range('a', 'z');
                        StringBuilder code = new StringBuilder();
                        code.Append(a);
                        code.Append(b);
                        code.Append(c);
                        code.Append(d);
                        CreateSession(code.ToString().ToUpper());
                        HostPort.code = code.ToString().ToUpper();
                        TMP_Text textmeshPro = jackbox.GetComponent<TextMeshProUGUI>();
                        Text startmeshpro = startCanvas.GetComponent<Text>();
                        textmeshPro.text = "     " + code.ToString().ToUpper();
                        startmeshpro.text = "Код успешно сгенерирован\nВзгляните на монитор напротив";
                        startOnce = true;
                        //Debug.Log(code.ToString());
                    }

                    if (hit.collider.gameObject.name == "ExitCanvas")
                    {
                        // save any game data here
                        #if UNITY_EDITOR
                        // Application.Quit() does not work in the editor so
                        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                        UnityEditor.EditorApplication.isPlaying = false;
                        #else
                        Application.Quit();
                        #endif
                    }



                    LaserSend.Play();
                    PostRequest(command);
                }
                    
            }
            else
            {
                lineRenderer.SetPosition(1, endPosition);
                //level0
                if (SceneManager.GetActiveScene().name == "Level0")
                {
                    var image = StartButton.GetComponent<Image>();
                    image.color = Color.black;
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
            if (SceneManager.GetActiveScene().name == "Level0")
            {
                var image = StartButton.GetComponent<Image>();
                image.color = Color.black;
                var image2 = ExitCanvas.GetComponent<Image>();
                image2.color = Color.black;
            }

        }
    }
}
