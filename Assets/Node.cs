using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private string _data;
    public string Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private Node _next;
    public Node Next
    {
        get { return _next; }
        set { _next = value; }
    }


    private Node _prev;
    public Node Prev
    {
        get { return _prev; }
        set { _prev = value; }
    }

    public Node(string data)
    {
        Data = data;
    }
}
