using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class protoTypeUI : MonoBehaviour
{
    public Canvas img;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (img.enabled == true)
            {
                img.enabled = false;
            }
            else { img.enabled = true; }
        }
    }
    public void mainMenuPressed() {

        SceneManager.LoadScene(0);
    
    }
}
