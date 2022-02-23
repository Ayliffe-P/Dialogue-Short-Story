using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FactorySpawner : MonoBehaviour
{
    public Transform[] locations;
    public GameObject[] bots;
    public Sprite one;
    System.Random rnd = new System.Random();


    void Start()
    {
        // GameObject temp = new GameObject();

        //   temp.transform.position = locations[0].transform.position;
        //   temp.AddComponent<NPCTypeOne>();
        // temp.AddComponent<NavMeshAgent>();

        // Instantiate(temp);
        for (int i = 0; i < bots.Length; i++)
        {

            bots[i] = randomNPC();
            bots[i].transform.position = locations[i].transform.position;
            bots[i].transform.rotation = Random.rotation;

        }

        InvokeRepeating("moveNPC", 5, 10);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public class NPCTypeOne : NPC {
        void Start()
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1);
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.radius = 0.19f;
            agent.height = 0.5f;
            agent.baseOffset = 0.27f;
            agent.stoppingDistance = 1.5f;

            spriteR = gameObject.AddComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Man 2 Sprite Sheet");

            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().radius = 0.11f;
            gameObject.GetComponent<CapsuleCollider>().height = 0.57f;

            anim = gameObject.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Man 2 Sprite Sheet_0");

            gameObject.tag = "NPC";
            gameObject.AddComponent<NewDialogueTrigger>();
            gameObject.GetComponent<NewDialogueTrigger>().dialogue = RandomScriptableDialogue();

            target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
            anim = GetComponent<Animator>();

            

        }
    }

    public class NPCTypeTwo : NPC
    {
        void Start()
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1);

            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().radius = 0.11f;
            gameObject.GetComponent<CapsuleCollider>().height = 0.57f;

            spriteR = gameObject.AddComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Woman Spritesheet");

            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.radius = 0.19f;
            agent.height = 0.5f;
            agent.baseOffset = 0.27f;
            agent.stoppingDistance = 1.5f;

            anim = gameObject.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Woman Spritesheet_0");

            gameObject.tag = "NPC";
            gameObject.AddComponent<NewDialogueTrigger>();
            gameObject.GetComponent<NewDialogueTrigger>().dialogue = RandomScriptableDialogue();

            target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
            anim = GetComponent<Animator>();

          

        }
    }

    public class NPCTypeThree : NPC
    {
        void Start()
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1);

            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().radius = 0.11f;
            gameObject.GetComponent<CapsuleCollider>().height = 0.57f;

            spriteR = gameObject.AddComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Girl 2 Sprite Sheet 1");

            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.radius = 0.19f;
            agent.height = 0.5f;
            agent.baseOffset = 0.27f;
            agent.stoppingDistance = 1.5f;

            anim = gameObject.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Girl 2 Sprite Sheet 1_0");

            gameObject.tag = "NPC";
            gameObject.AddComponent<NewDialogueTrigger>();
            gameObject.GetComponent<NewDialogueTrigger>().dialogue = RandomScriptableDialogue();

            target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
            anim = GetComponent<Animator>();

          

        }
    }

    public class NPCTypeFour : NPC
    {
        void Start()
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1);

            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().radius = 0.11f;
            gameObject.GetComponent<CapsuleCollider>().height = 0.57f;

            spriteR = gameObject.AddComponent<SpriteRenderer>();
            spriteR.sprite = Resources.Load<Sprite>("Sprites/Man 3 Sprite Sheet");

            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.radius = 0.19f;
            agent.height = 0.5f;
            agent.baseOffset = 0.27f;
            agent.stoppingDistance = 1.5f;

            anim = gameObject.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/Man 3 Sprite Sheet_0");

            gameObject.tag = "NPC";
            gameObject.AddComponent<NewDialogueTrigger>();
            gameObject.GetComponent<NewDialogueTrigger>().dialogue = RandomScriptableDialogue();

            target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
            anim = GetComponent<Animator>();

            

        }
    }

    public GameObject randomNPC() {
        int temp = rnd.Next(0, 4);
        GameObject npc = new GameObject();
        switch (temp) {
            case 0:
                npc.AddComponent<NPCTypeOne>();
                return npc;
            case 1:
                npc.AddComponent<NPCTypeTwo>();
                return npc;
            case 2:
                npc.AddComponent<NPCTypeThree>();
                return npc;
            case 3:
                npc.AddComponent<NPCTypeFour>();
                return npc;
        }
        Debug.Log("Null");
        return null;
    }

    public void moveNPC() {
        foreach (var item in bots)
        {
            StartCoroutine(item.GetComponent<NPC>().moveNPC(getLocation()));
        }
    }

    public Transform getLocation() {
        return locations[rnd.Next(0, 8)];
    }

}
