using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.AI;

public class protoTypeDialogueManager : MonoBehaviour
{
    //singletoooon
    private static protoTypeDialogueManager _pdm;
    public static protoTypeDialogueManager ptDm { get { return _pdm; } }

    private void Awake()
    {
        if (_pdm != null && _pdm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _pdm = this;
        }
    }


    private ScriptableDialogue activeDialogue;
   
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
    

   

    private void Start()
    {
        Option1.onClick.AddListener(Path1);
        Option2.onClick.AddListener(Path2);
        
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
        //set current node ID to the first nodeID
        currentNode = activeDialogue.NodeData.Where(x => x.nodeID == leadingEdge.secondNodeID).First();

        normalText.enabled = true;
        normalText.text = currentNode.dialogueTxt;
        //StartCorsoutine(typeSentence(currentNode.dialogueTxt));
    }

    public void NextDialogue()
    {
        Debug.Log("Next Dialogue");
        //if node is end node, end dialogue
        if (currentNode.dialogueTxt == "End")
        {
            EndDialogue();
        }
        
        //if has 2 edges
        //activeDialogue.ReturnValidEdges(currentNode).Count >= 2
        else if (activeDialogue.ReturnPotentialEdges(currentNode) >= 2)
        {
            //but only has one valid edge
            if (activeDialogue.ReturnValidEdges(currentNode).Count == 1)
            {
                if (isNode)
                {
                    //display node
                    thisIsNode();
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
                    //display node
                    thisIsNode();
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
        //if has 1 edge
        else if (activeDialogue.ReturnPotentialEdges(currentNode) == 1)
        {
            if (!isNode)
            {
                //display edge
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
                //display node
                thisIsNode();
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
        //moves onto next node
        currentNode = activeDialogue.FindNode(activeDialogue.ReturnValidEdges(currentNode)[0].secondNodeID);
        isNode = true;

        NextDialogue();
    }

    public void Path2()
    {
        //moves onto next node
        currentNode = activeDialogue.FindNode(activeDialogue.ReturnValidEdges(currentNode)[1].secondNodeID);
        isNode = true;
        NextDialogue();
    }

    public void thisIsNode()
    {
        Debug.Log("Noding");
        //display node
        normalText.enabled = true;
        Option1.gameObject.SetActive(false);
        Option2.gameObject.SetActive(false);

        normalText.text = currentNode.dialogueTxt;

        isNode = false;
    }
}
