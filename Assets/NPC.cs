using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum NPCType { Type1, Type2, Type3, Type4 }
public abstract class NPC : MonoBehaviour
{

    
    public Transform target;
    public Animator anim;
    public SpriteRenderer spriteR;
    public static System.Random rnd = new System.Random();
    public NavMeshAgent agent;

    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
        anim = GetComponent<Animator>();
        gameObject.AddComponent<SpriteRenderer>();
        Debug.Log("START HAS BEEN OVERRIDEN");
        

    }
    
    public ScriptableDialogue RandomScriptableDialogue() {

        int num = rnd.Next(1, 5);

        switch (num)
        {
            case 1:
                return Resources.Load<ScriptableDialogue>("fluffText1");
               
            case 2:
                return Resources.Load<ScriptableDialogue>("fluffText2");
            case 3:
                return Resources.Load<ScriptableDialogue>("fluffText3");
            case 4:
                return Resources.Load<ScriptableDialogue>("fluffText4");

        }


        return null;

    
    }

    bool CheckIfDestinationReached()
    {

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator moveNPC(Transform trans) {

        agent.SetDestination(trans.position);
        anim.SetBool("Walk", true);
        anim.SetBool("Dancing", false);
    

        while (CheckIfDestinationReached() == false)
        {
            yield return null;
        }
        anim.SetBool("Walk", false);
        anim.SetBool("Dancing", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;

        if (Vector3.Dot(toTarget, transform.forward) > 0)
        {
  
            anim.SetBool("FacingFront", true);
        }
        else
        {

            anim.SetBool("FacingFront", false);
        }
        
    }
}
