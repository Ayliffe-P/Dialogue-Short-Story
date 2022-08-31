using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GraphDialogueTrigger : MonoBehaviour
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
        GraphDialogueManager.GDM.activateDialogue(dialogue);
        enable = true;
    }

    private void Update()
    {
        
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionDist);
    }
}
