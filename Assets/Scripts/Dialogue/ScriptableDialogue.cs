using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]

public class ScriptableDialogue : ScriptableObject
{
    public List<EdgeData> GraphEdges = new List<EdgeData>();
    public List<DialogueNodeData> NodeData = new List<DialogueNodeData>();
    int totalEdges;
    
   
    public List<EdgeData> ReturnValidEdges(DialogueNodeData current)
    {
        List<EdgeData> edges = GraphEdges.Where(x=>x.firstNodeID == current.nodeID ).ToList();
        List<DialogueNodeData> nodes = new List<DialogueNodeData>();

        edges.ForEach(e => NodeData.Where(n => n.nodeID == e.secondNodeID && parser.P.inv.ContainsKey(Int32.Parse(n.dialogueKey))).ToList().ForEach(n => nodes.Add(n)));


        List<EdgeData> validEdges = new List<EdgeData>();
        nodes.ForEach(n=>edges.Where(e=>e.secondNodeID == n.nodeID).ToList().ForEach(e=>validEdges.Add(e)));


        return validEdges;
    }

    public DialogueNodeData FindNode(string NodeID)
    {
        return NodeData.Where(n=>n.nodeID == NodeID).ToList().First();
    }

    public int ReturnPotentialEdges(DialogueNodeData current)
    {
        List<EdgeData> edges = GraphEdges.Where(x => x.firstNodeID == current.nodeID).ToList();
        List<DialogueNodeData> nodes = new List<DialogueNodeData>();

        edges.ForEach(e => NodeData.Where(n => n.nodeID == e.secondNodeID).ToList().ForEach(n => nodes.Add(n)));

        List<EdgeData> validEdges = new List<EdgeData>();
        nodes.ForEach(n => edges.Where(e => e.secondNodeID == n.nodeID).ToList().ForEach(e => validEdges.Add(e)));

        totalEdges = validEdges.Count();
        return totalEdges;
    }

}
