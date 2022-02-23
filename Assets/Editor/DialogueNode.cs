using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//in order to make the graphview
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//had to use this since i already made a class for Node (linked lists) that it kept using instead
public class DialogueNode : UnityEditor.Experimental.GraphView.Node
{
    public string nodeID;

    public string dialogueText;
    //first node - node that everything has to come frm
    public bool entryPoint = false;

    public string key;
}
