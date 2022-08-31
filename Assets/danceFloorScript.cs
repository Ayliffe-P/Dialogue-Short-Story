using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danceFloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entering Dance floor");
            other.gameObject.GetComponent<Animator>().SetBool("Dancing", true);
            if (ObjectiveManager._OBJMAN.contains("Go to the dance floor"))
            {
                ObjectiveManager._OBJMAN.tasks.Remove("Go to the dance floor");
                ObjectiveManager._OBJMAN.tasks.Add("Take a drink");

            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Dancing", false);
        }
    }
    
}
