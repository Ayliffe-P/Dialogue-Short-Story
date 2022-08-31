using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.AI;

public class GraphDialogueManager : MonoBehaviour
{
    private static GraphDialogueManager _gdm;
    public static GraphDialogueManager GDM { get { return _gdm; } }

    private void Awake()
    {
        if (_gdm != null && _gdm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _gdm = this;
        }
    }

    
    private ScriptableDialogue activeDialogue;
    public GameObject barman;
    private EdgeData _edgeData;
    private DialogueNodeData currentNode;

    public bool inDialogue;
    public bool isNode;

    public Text normalText;
    public Text nameText;
    public Text option1;
    public Text option2;

    public GameObject dialogueBox;

    public Button Option1;
    public Button Option2;

    public bool dialogueEnd = false;
    public bool spawnDrink = false;

    public UIFadeout FadeManager;

    private void Start()
    {
        Option1.onClick.AddListener(Path1);
        Option2.onClick.AddListener(Path2);
        barman = GameObject.Find("Barman");
        Debug.Log("set paths");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && inDialogue)
        {
            Debug.Log("Clicking");
            NextDialogue();
        }
    }
    public void activateDialogue(ScriptableDialogue DialIn)
    {
        if (!inDialogue)
        {
            activeDialogue = DialIn;
            inDialogue = true;
            dialogueBox.SetActive(true);
            StartDialogue();
        }
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }
    

    public void StartDialogue()
    {
        EdgeData leadingEdge = activeDialogue.GraphEdges.Where(x => x.portName == "StartNodeEdge").First();
        currentNode = activeDialogue.NodeData.Where(x => x.nodeID == leadingEdge.secondNodeID).First();
        normalText.enabled = true;
        normalText.text = currentNode.dialogueTxt;
    
    }

    public void NextDialogue()
    {
        Debug.Log("Next Dialogue");
        if (currentNode.dialogueTxt == "End")
        {
            EndDialogue();
        }
        if (currentNode.dialogueTxt == "EndBarman")
        {
            GetComponent<InventoryManager>().spawnDrink();
            EndDialogue();

        }
        if (currentNode.dialogueTxt == "EndOne") {
            StartCoroutine(GameObject.Find("Shane").GetComponent<ShaneScript>().moveToBar());
            EndDialogue();
        }

        if (currentNode.dialogueTxt == "EndTwo")
        {
            StartCoroutine(GameObject.Find("Shane").GetComponent<ShaneScript>().moveToDanceFloor());
            EndDialogue();
        }
        if (currentNode.dialogueTxt == "EndThree")
        {
            EndDialogue();
            StartCoroutine(FadeManager.FadeOut(true));
           
        }
        else if (activeDialogue.ReturnPotentialEdges(currentNode) >= 2)
        {
            if (activeDialogue.ReturnValidEdges(currentNode).Count == 1)
            {
                if (isNode)
                {
                    thisIsANode();
                }
                else
                {
                    normalText.enabled = false;
                    Option1.gameObject.SetActive(true);
                    Option2.gameObject.SetActive(true);

                    option1.text = activeDialogue.ReturnValidEdges(currentNode)[0].portName;
                    option2.text = "Option requires an item to unlock it.";
                }
            }
            else if (activeDialogue.ReturnValidEdges(currentNode).Count >= 2)
            {
                if (isNode)
                {
                    thisIsANode();
                }
                else
                {
                    normalText.enabled = false;
                    Option1.gameObject.SetActive(true);
                    Option2.gameObject.SetActive(true);

                    option1.text = activeDialogue.ReturnValidEdges(currentNode)[0].portName;
                    option2.text = activeDialogue.ReturnValidEdges(currentNode)[1].portName;
                }
            }
        }
        else if (activeDialogue.ReturnPotentialEdges(currentNode) == 1)
        {
            if (!isNode)
            {
                normalText.enabled = true;
                Option1.gameObject.SetActive(false);
                Option2.gameObject.SetActive(false);
                Debug.Log(activeDialogue.ReturnValidEdges(currentNode).Count);
                normalText.text = activeDialogue.ReturnValidEdges(currentNode)[0].portName;

                currentNode = activeDialogue.FindNode(activeDialogue.ReturnValidEdges(currentNode)[0].secondNodeID);
                isNode = true;
            }
            else if (isNode)
            {
                thisIsANode();
            }
        }
    }
   

    public void EndDialogue()
    {
        inDialogue = false;
        dialogueEnd = true;
        dialogueBox.SetActive(false);
        Option1.gameObject.SetActive(false);
        Option2.gameObject.SetActive(false);
    }

    public void Path1()
    {
        Debug.Log("Path 1");
        currentNode = activeDialogue.FindNode(activeDialogue.ReturnValidEdges(currentNode)[0].secondNodeID);
        isNode = true;
        NextDialogue();
    }

    public void Path2()
    {
       currentNode = activeDialogue.FindNode(activeDialogue.ReturnValidEdges(currentNode)[1].secondNodeID);
        isNode = true;
        NextDialogue();
    }

    public void thisIsANode()
    {
        Debug.Log("Node");
        normalText.enabled = true;
        Option1.gameObject.SetActive(false);
        Option2.gameObject.SetActive(false);

        normalText.text = currentNode.dialogueTxt;

        isNode = false;
    }
}
