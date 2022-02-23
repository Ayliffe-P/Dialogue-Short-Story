using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;

public class GraphSaveLoad
{
    //instances
    private DialogueGV _targetGV;
    private ScriptableDialogue _scCache;

    //list of edges in gv to get access to connections in gv
    private List<Edge> Edges => _targetGV.edges.ToList();
    //cast them to dialogue nodes
    private List<DialogueNode> Nodes => _targetGV.nodes.ToList().Cast<DialogueNode>().ToList();
        
    
    public static GraphSaveLoad GetInstance(DialogueGV targetGV)
    {
        return new GraphSaveLoad
        {
            _targetGV = targetGV
        };
    }

    //method to save graph
    public void SaveGraph(string fileName)
    {
        //if there are no edges then we wont save
        if (!Edges.Any())
        {
            return;
        }

        var scriptableDialogue = ScriptableObject.CreateInstance<ScriptableDialogue>();

        //saving connections between nodes
        //if an ouput port is connected to inut we count it as valid connected port
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            //adding new data to edge - the first node will have the output node id, portname will have the name of the port thta is connected and the second node will have the id of that node
            scriptableDialogue.GraphEdges.Add(new EdgeData
            {
                firstNodeID = outputNode.nodeID,
                portName = connectedPorts[i].output.portName,
                secondNodeID = inputNode.nodeID
            });
        }
        //iterate over all nodes except entry node - entry node will always be there
        foreach (var dialogueNode in Nodes.Where(node=>!node.entryPoint))
        {
            //
            scriptableDialogue.NodeData.Add(new DialogueNodeData
            {
                nodeID = dialogueNode.nodeID,
                dialogueTxt = dialogueNode.dialogueText,
                dialogueKey = dialogueNode.key,
                position = dialogueNode.GetPosition().position
            });
        }
        //create and save scriptable object to resources folder
        AssetDatabase.CreateAsset(scriptableDialogue, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    //method to load graph
    public void LoadGraph(string fileName)
    {
        //load file from resources and assign to cache 
        _scCache = Resources.Load<ScriptableDialogue>(fileName);
        //does it exist?
        if (_scCache == null)
        {
            //show error message
            EditorUtility.DisplayDialog("File doesn't exist.", "This file does not exist.", "OK");
            return;
        }
        //clear existing graph
        ClearGraph();
        CreateNodes();
        CreateEdges();
    }

    private void ClearGraph()
    {
        //find entry point and set it to the first node id (since its always first) and discard existing node id
        Nodes.Find(x => x.entryPoint).nodeID = _scCache.GraphEdges[0].firstNodeID;

        //loop through all nodes
        foreach (var node in Nodes)
        {
            //except entry node
            if (node.entryPoint)
            {
                //instead of return - skip rather than stop the loop
                continue;
            }
            
            //remove edges connected to this node
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGV.RemoveElement(edge));
            //remove the node itself
            _targetGV.RemoveElement(node);
        }
    }

    private void CreateNodes()
    {
        //iterate over all dialogue node data
        foreach (var nodeData in _scCache.NodeData)
        {
            //create new temporary node
            var loadNode = _targetGV.CreateDialogueNode(nodeData.dialogueTxt, nodeData.dialogueKey);
            //change id to save node id
            loadNode.nodeID = nodeData.nodeID;
            //loads key

            //add node to graph view
            _targetGV.AddElement(loadNode);

            //adding choice ports to list
            var nodePorts = _scCache.GraphEdges.Where(x => x.firstNodeID == nodeData.nodeID).ToList();
            //iterate trough list to add choice ports to node
            nodePorts.ForEach(x => _targetGV.AddChoicePort(loadNode, x.portName));
        }
    }

    private void CreateEdges()
    {
        //loop through all the nodes
        for (var i = 0; i < Nodes.Count; i++)
        {
            //get edges form cache file and match the ids of current and output nodes
            //to see if node has any connections saved into file
            var connections = _scCache.GraphEdges.Where(x => x.firstNodeID == Nodes[i].nodeID).ToList();

            //nested loop to loop through all the connections
            for (var j = 0; j < connections.Count; j++)
            {
                var secNodeID = connections[j].secondNodeID;
                var secondNode = Nodes.First(x => x.nodeID == secNodeID);
                //call method to link ports together
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)secondNode.inputContainer[0]);

                secondNode.SetPosition(new Rect(_scCache.NodeData.First(x=>x.nodeID == secNodeID).position, _targetGV.defaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge { output = output, input = input };

        //connect them to each other
        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        //add this edge to graph
        _targetGV.Add(tempEdge);
    }
}
