using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShaneScript : MonoBehaviour
{

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator moveToBar() { 
    agent.SetDestination(GameObject.Find("Barman").transform.GetChild(0).position);
        ObjectiveManager._OBJMAN.tasks.Remove("Talk to Shane");

        while (CheckIfDestinationReached() == false) {
            yield return null;
        } 
        GameObject.Find("Manager").GetComponent<InventoryManager>().spawnDrink();
        GetComponent<NavMeshAgent>().ResetPath();
        Debug.Log("Working");
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
