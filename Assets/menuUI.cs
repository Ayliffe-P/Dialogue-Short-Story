using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuUI : MonoBehaviour
{
    public Button playButton;
    public Button graphbutton;
    public Button quitButon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayPressed() {

        SceneManager.LoadScene(1);
    
    }
    public void onExplanationPressed()
    {

        SceneManager.LoadScene(3);

    }
    public void onGraphPressed()
    {

        SceneManager.LoadScene(4);

    }
    public void onQuitPressed()
    {

        Application.Quit();

    }
}
