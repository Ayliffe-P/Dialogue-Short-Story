using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    private static ObjectiveManager objMan;
    public static ObjectiveManager _OBJMAN { get { return objMan; } }

    private void Awake()
    {
        if (objMan != null && objMan != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            objMan = this;
        }
    }

    public Text objectiveText;

    public List<string> tasks;
    
    void Start()
    {
        tasks.Add("Talk to Shane");
        tasks.Add("Get a drink");
        
    }

    // Update is called once per frame
    void Update()
    {
        changeText(tasks);
    }

    public void changeText(List<string> text) {
        objectiveText.text = "";
        foreach (var item in text)
        {

            objectiveText.text += ") " + item + "\n";
        }

    }

    public void addToEnd() { 
    
    
    }



}
