using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.AI;

public class NewDialogueManager : MonoBehaviour
{
    //singletoooon
    private static NewDialogueManager _dlm;
    public static NewDialogueManager DLM { get { return _dlm; } }

    private void Awake()
    {
        if (_dlm != null && _dlm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _dlm = this;
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
        if (currentNode.dialogueTxt == "EndBarman")
        {
            GetComponent<InventoryManager>().spawnDrink();
            EndDialogue();

        }
        if (currentNode.dialogueTxt == "EndOne") {
            StartCoroutine(GameObject.Find("Shane").GetComponent<ShaneScript>().moveToBar());
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

    //just being fancy, making text display as if it's being typed
    //IEnumerator typeSentence(string sentence)
    //{
    //    normalText.text = "";
    //    foreach (char letter in sentence.ToCharArray())
    //    {
    //        normalText.text += letter;
    //        yield return new WaitForSeconds(0.04f);
    //    }
    //}
}
