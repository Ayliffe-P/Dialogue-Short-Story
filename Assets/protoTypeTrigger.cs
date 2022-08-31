using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class protoTypeTrigger : MonoBehaviour
{

    [SerializeField]
    public ScriptableDialogue dialogue;

    [SerializeField]
    float distance;

    private Transform player;
    private bool enable = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void start()
    {
        protoTypeDialogueManager.ptDm.activateDialogue(dialogue);
        enable = true;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= distance && !enable && player)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
             protoTypeDialogueManager.ptDm.activateDialogue(dialogue);
             enable = true;
         }
        }
        else if (Vector3.Distance(transform.position, player.position) > distance && enable)
        {
           protoTypeDialogueManager.ptDm.EndDialogue();
           enable = false;
        }
       

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
