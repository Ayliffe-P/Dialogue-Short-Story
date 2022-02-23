using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
     public static DialogueManager instance;

     public Text NPCText;
     public Text PlayerText;
     public GameObject PlayerBox;
     public GameObject NPCBox;
     public Text NPCName;
     public List<string> Names;

     bool inDialogue;

     Dialogue dialogue;

     //[SerializeField]DoubleLinkedList Dlist = new DoubleLinkedList();

     void Start()
     {
         if (instance != null && instance != this)
         {
             Destroy(this.gameObject);
         }
         else
         {
             instance = this;
         }

         Names.Add("Player");
         Names.Add("Arnas");
         Names.Add("Depression");
     }

     public void DialogueStart(Dialogue dlIn)
     {
       /* Debug.Log("starting");
         inDialogue = true;
         dialogue = dlIn;
         //dialogue.list.GetNext(Dlist.Active);
         Debug.Log("First is " + dialogue.list.First);

         if (SplitText(dialogue.list.Active.getText())[0] == "0")
         {
             NPCBox.SetActive(false);
             PlayerBox.SetActive(true);

             PlayerText.text = SplitText(dialogue.list.Active.getText())[1];
         }*/

     }
     public void NextDialogue()
     {/*
         if (dialogue.list.Active.getNextNode() != null)
         {
            // dialogue.list.GetNext(Dlist.Active);

             //if player
             if (SplitText(dialogue.list.Active.getText())[0] == "0")
             {
                 NPCBox.SetActive(false);
                 PlayerBox.SetActive(true);

                 PlayerText.text = SplitText(dialogue.list.Active.getText())[1];
             }
             //if NPC
             else
             {
                 PlayerBox.SetActive(false);
                 NPCBox.SetActive(true);

                 NPCName.text = Names[int.Parse(SplitText(dialogue.list.Active.getText())[0])];


                NPCText.text =  SplitText(dialogue.list.Active.getText())[1];
             }
         }
         else
         {
             EndDialogue();
         }*/
     }

     public void EndDialogue()
     {
         Debug.Log("Ended");
         PlayerBox.SetActive(false);
         NPCBox.SetActive(false);
         inDialogue = false;
     }
     public string[] SplitText(string lineIn)
     {
         string[] output = lineIn.Split('#');

         return output;
     }

     // Update is called once per frame
     void Update()
     {
         if (Input.GetMouseButtonDown(0) && inDialogue)
         {
             NextDialogue();
         }
     }
   
}
