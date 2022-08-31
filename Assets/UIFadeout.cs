using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFadeout : MonoBehaviour
{
    public GameObject square;
    void Start()
    {
        
    }

    public IEnumerator FadeOut(bool fadeToBlack = true, int fadeSpeed = 5) {
        Color objectColor = square.GetComponent<Image>().color;
        float fadeAmount;
        while (square.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            square.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        SceneManager.LoadScene(3);
    }
    void Update()
    {
        
    }
}
