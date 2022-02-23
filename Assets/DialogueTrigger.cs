using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private Transform player;
    [SerializeField] float interactionDist;
    private bool enable = false;
    public string dialogueFile;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }



    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= interactionDist && !enable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
                enable = true;
            }
        }

    }
    public void TriggerDialogue()
    {
        string path = Application.dataPath + @"\Dialogue Text Files\" + dialogueFile + @".txt";
        DialogueManager.instance.DialogueStart(new Dialogue(path));
    }
}
