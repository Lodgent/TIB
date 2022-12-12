using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public string LevelName;
    public GameObject player;

    public float x;
    public float y;
    public float z;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(LevelName);
            GiveCommand.StaticPostRequest("CompleteLevel");
            player.transform.position = new Vector3(x, y, z);
        }
    }
}
