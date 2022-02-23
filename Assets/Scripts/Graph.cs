using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public class GraphNodes
    {
        string words;
        string name;
        string nextNode1;
        string nextNode2;
        string nodeID;
        string prevNode;
    }

    public class Edge
    {
        GraphNodes nextNode;
        public GraphNodes getNextNode()
        {
            return nextNode;
        }
    }

    public void AddNode()
    {

    }
    
    public void AddEdge()
    {

    }
}
