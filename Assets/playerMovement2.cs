using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class playerMovement2 : MonoBehaviour
{

    float maxDistance = 2.5f;
    public float Speed = 3.0F;
    public float RotateSpeed = 3.0F;
    public Animator anim;
    NavMeshAgent mNavMeshAgent;
    void Update()
    {
        
        Vector3 offset = new Vector3(0.0f, 0.3f, 0.0f);
       Debug.DrawRay(transform.position + offset, transform.forward * 2.5f, Color.green);
        CharacterController controller = GetComponent<CharacterController>();
        if (transform != null)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * RotateSpeed, 0);
            var forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = Speed * Input.GetAxis("Vertical");
            //transform.position += Vector3.forward * Time.deltaTime * Speed;
             controller.SimpleMove(forward * curSpeed);
            
        }

        
        if (!mNavMeshAgent.pathPending)
        {
            if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
            {
                if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    GetComponent<NavMeshAgent>().ResetPath();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            shootRay();
        }

            /*  if (Input.GetKey(KeyCode.W))
              {
                  anim.SetBool("isWalking", true);
              }
              else { anim.SetBool("isWalking", false); }
          }*/
        }
    void FixedUpdate()
    {
       /* Vector3 offset = new Vector3(0.0f, 0.3f, 0.0f);
       
        // Will contain the information of which object the raycast hit
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (Physics.Raycast(transform.position + offset, transform.forward, out hit, maxDistance) &&
                    hit.collider.gameObject.CompareTag("NPC"))
                

            {
                Debug.Log("" + hit.collider.gameObject.tag);
                hit.collider.gameObject.GetComponent<NewDialogueTrigger>().start();




            }
            if (Physics.Raycast(transform.position + offset, transform.forward, out hit, maxDistance) &&
                   hit.collider.gameObject.CompareTag("Barman"))
            {
                Debug.Log("" + hit.collider.gameObject.tag);
                hit.collider.gameObject.GetComponent<NewDialogueTrigger>().start();
            }
        }
        // if raycast hits, it checks if it hit an object with the tag Player
        
        */
    }

    public void shootRay() {
        Vector3 offset = new Vector3(0.0f, 0.3f, 0.0f);
       Debug.DrawRay(transform.position + offset, transform.forward * 1.5F, Color.green);
        // Will contain the information of which object the raycast hit
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (Physics.Raycast(transform.position + offset, transform.forward, out hit, maxDistance) &&
                    hit.collider.gameObject.CompareTag("NPC"))


            {
                Debug.Log("" + hit.collider.gameObject.tag);
                hit.collider.gameObject.GetComponent<NewDialogueTrigger>().start();




            }
            if (Physics.Raycast(transform.position + offset, transform.forward, out hit, maxDistance) &&
                   hit.collider.gameObject.CompareTag("Barman"))
            {
                Debug.Log("" + hit.collider.gameObject.tag);
                hit.collider.gameObject.GetComponent<NewDialogueTrigger>().start();
            }
           
        }
    }
    private void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
       
    }

}