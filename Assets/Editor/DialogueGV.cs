using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//in order to make the graphview
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class DialogueGV : GraphView
{
    //making use of this video for creating a graph view to implement the graph dialogue 
    //(video is only for making graph, not implementing it into game)
    //https://www.youtube.com/watch?v=7KHGH0fPL84

    //default node size 
    public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    //constructor
    public DialogueGV()
    {
        //screen go zoooom
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        //content dragger, selection dragger and rectangle selector
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        //add element of entry point node to graph view
        AddElement(CreateEntryPointNode());
    }

    //creating a port
    //takes n the node, the direction the port is going to, and the cpacity of the port
    private Port CreatePort(DialogueNode node, Direction portDirection, Port.Capacity portCapacity = Port.Capacity.Single)
    {
        //not transmitting data between ports so type is arbitrary
        return node.InstantiatePort(Orientation.Horizontal, portDirection, portCapacity, typeof(float));
    }

    //creating the first node
    private DialogueNode CreateEntryPointNode()
    {
        //new node and what it contains
        var node = new DialogueNode
        {
            //node name
            title = "Start",
            //makng a new unique id for it
            nodeID = Guid.NewGuid().ToString(),
            //the dialogue text
            dialogueText = "EntryPoint",
            //whether or not this is the entry node
            entryPoint = true
        };

        var createdPort = CreatePort(node, Direction.Output);
        createdPort.portName = "StartNodeEdge";
        //output container for this new node
        node.outputContainer.Add(createdPort);

        //changing this entry point node capbilities to not be able to be moved or deleted
        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;


        //refresh after adding ports to update the visuals 
        node.RefreshExpandedState();
        node.RefreshPorts();

        //setting the pos of the node in the graph view
        node.SetPosition(new Rect(100, 200, 200, 150));
        //return the node
        return node;
    }

    //basically the same as enry node, but for dialogue so some changes such as multiple ports
    public DialogueNode CreateDialogueNode(string nodeName, string keyOverride = "000")
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            dialogueText = nodeName,
            key = keyOverride,
            nodeID = Guid.NewGuid().ToString()
        };

        //can have multiple ports
        var inputPort = CreatePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        //actual dialogue text
        var dialText = new TextField(string.Empty);
        dialText.RegisterValueChangedCallback(evt => 
        {
            dialogueNode.dialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });
        dialText.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(dialText);

        //adds key input for inv check
        var keyText = new TextField("Key: ");
        keyText.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.key = evt.newValue;
        });
        keyText.SetValueWithoutNotify(dialogueNode.key);
        dialogueNode.mainContainer.Add(keyText);

        //clicking the button to add a new choice port
        var button = new Button(clickEvent: () => { AddChoicePort(dialogueNode); });
        //adding the choice button to allow us to add a new port that will represent a new dialogue choice
        button.text = "Add Choice";
        dialogueNode.titleContainer.Add(button);

        //refresh after adding ports to update the visuals 
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();

        //setting the pos of the node in the graph view
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        //return the node
        return dialogueNode;
    }

    //orPortName = overriden
    public void AddChoicePort(DialogueNode dialogueNode, string orPortName = "")
    {
        //creating a port
        var createdPort = CreatePort(dialogueNode, Direction.Output);

        //query for old label - looking for label anmed type
        var label = createdPort.contentContainer.Q<Label>("type");
        //remove it
        createdPort.contentContainer.Remove(label);

        //change port name for each option
        //search for all ports in output container of this node
        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
        //assign port name as choice and the number of the port (eg Choice 1, Choice 2)
        createdPort.portName = $"Type Choice Here {outputPortCount}";

        // if true do whats after ? else whats after :
        var choicePortName = string.IsNullOrEmpty(orPortName)
            ? $"Type Choice Here {outputPortCount + 1}"
            : orPortName;

        var choiceText = new TextField
        {
            name = string.Empty,
            //choice portname as def val
            value = choicePortName
        };
        //callback to change port name when we change the text field
        choiceText.RegisterValueChangedCallback(evt => createdPort.portName = evt.newValue);
        createdPort.contentContainer.Add(new Label("    "));
        createdPort.contentContainer.Add(choiceText);

        //delete button to delete choice
        var delChoice = new Button(()=>RemovePort(dialogueNode, createdPort)){text = "X"};

        //add button to content container
        createdPort.contentContainer.Add(delChoice);

        createdPort.portName = choicePortName;

        //add this port to output container
        dialogueNode.outputContainer.Add(createdPort);

        //refresh after adding ports to update the visuals 
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }

    //creating the node so that it dsplays on the graph view
    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    //a list of which ports are compatible - for connecting ports to nodes to create edges
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        //dont want to connect port to itself, or a node input port to its own output port (infinite loop)
        //if conditions are met, add it to compatible list
        ports.ForEach(funcCall: (port) =>
            {
                if (startPort !=port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            }
        );
        
        return compatiblePorts;
    }

    private void RemovePort(DialogueNode dialogueNode, Port createdPort)
    {
        //
        var targetEdge = edges.ToList().Where(x => x.output.portName == createdPort.portName && x.output.node == createdPort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            //dsiconnect connections (removes visual edge line)
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }
        
        //remove from output container
        dialogueNode.outputContainer.Remove(createdPort);

        //refresh to update the visuals 
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }
}
