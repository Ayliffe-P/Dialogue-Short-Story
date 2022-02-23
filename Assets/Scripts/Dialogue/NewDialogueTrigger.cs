using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class NewDialogueTrigger : MonoBehaviour
{
    private AudioSource typeSound;
   

    [SerializeField]
    public ScriptableDialogue dialogue;

    [SerializeField]
    float interactionDist;

    private Transform player;

    Animator NPCAnim;

    [SerializeField]
    Animator playerAnim;

    private bool enable = false;

    private void Awake()
    {
        typeSound = GetComponent<AudioSource>();
       
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        NPCAnim = GetComponent<Animator>();
    }
    public void start() {
        NewDialogueManager.DLM.activateDialogue(dialogue);
        enable = true;
       // NPCAnim.SetBool("Walk", false);
    }

    private void Update()
    {
           /*if (Vector3.Distance(transform.position, player.position) <= interactionDist && !enable && player)
           {
               if (Input.GetKeyDown(KeyCode.E))
               {
                NewDialogueManager.DLM.activateDialogue(dialogue);
                enable = true;
            }
           }
           else if (Vector3.Distance(transform.position, player.position) > interactionDist && enable)
           {
              NewDialogueManager.DLM.EndDialogue();
              enable = false;
           }
           */
        /* if (NewDialogueManager.DLM.dialogueEnd &&
            // SceneManager.GetActiveScene() == SceneManager.GetSceneByName("BEDROOM")&&
           //  RealParser.RP.check1True &&
            // RealParser.RP.check2True)
         {
             SceneManager.LoadScene(sceneName: "END1");
         }
        */
        /*  if (NewDialogueManager.DLM.inDialogue)
          {
              playerAnim.SetBool("Talk", true);
              if (typeSound.isPlaying) return;
              {
                  typeSound.Play();
              }
          }
          else if (!NewDialogueManager.DLM.inDialogue)
          {
              playerAnim.SetBool("Talk", false);
              typeSound.Stop();
          }*/
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionDist);
    }
}
