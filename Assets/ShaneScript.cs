using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShaneScript : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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

    public IEnumerator moveToBar() {
        anim.SetBool("Walk", true);
        anim.SetBool("Idle", false);
        agent.SetDestination(GameObject.Find("Barman").transform.GetChild(0).position);
        ObjectiveManager._OBJMAN.tasks.Remove("Talk to Shane");

        while (CheckIfDestinationReached() == false) {
            yield return null;
        } 
        GameObject.Find("Manager").GetComponent<InventoryManager>().spawnDrink();
        GetComponent<NavMeshAgent>().ResetPath();
        Debug.Log("Working");
        gameObject.GetComponent<GraphDialogueTrigger>().dialogue = Resources.Load<ScriptableDialogue>("mainDialogue2");
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", true);
    }
    public IEnumerator moveToDanceFloor()
    {
        ObjectiveManager._OBJMAN.tasks.Remove("Talk to Shane");
        ObjectiveManager._OBJMAN.tasks.Add("Go to the dance floor");
        anim.SetBool("Walk", true);
        anim.SetBool("Idle", false);
        agent.SetDestination(GameObject.Find("Dance Floor").transform.position);
        while (CheckIfDestinationReached() == false)
        {
            yield return null;
        }
        GetComponent<NavMeshAgent>().ResetPath();
        anim.SetBool("Walk", false);
        anim.SetBool("Dancing", true);
    }

    bool CheckIfDestinationReached() {

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

}
