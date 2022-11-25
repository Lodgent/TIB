using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorBloom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public AudioSource activated;
    private bool IsTriggered;
    public Light light;
    void Start()
    {
        IsTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (!IsTriggered)
        {
            Material mymat = gameObject.GetComponent<Renderer>().material;
            mymat.color = Color.green;
            mymat.SetColor("_EmissionColor", Color.green);
            activated.Play();
            light.color = Color.green;
            IsTriggered = true;
        }

    }

    
}
