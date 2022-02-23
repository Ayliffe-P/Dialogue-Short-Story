using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;

[Serializable]

public class EdgeData
{
    //first node
    public string firstNodeID;
    //port that connects nodes
    public string portName;
    //node that the first node is connected to 
    public string secondNodeID;
}
