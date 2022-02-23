using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLinkedList : MonoBehaviour
{


    private Node active = null;
    public Node Active { get { return active; } }

    private Node head;
    public Node First
    {
        get { return head; }
    }

    private Node tail;
    public Node Last
    {
        get { return tail; }
    }

    public int Length { get; private set; }

    //-----------------------ADD (ADD LAST)-----------------------//
    public void Add(string data)
    {
        Node newNode = new Node(data);
        if (tail == null)
        {
            head = newNode;
        }
        else
        {
            newNode.Prev = tail;
            tail.Next = newNode;
        }

        tail = newNode;
        Length++;
    }

    //-----------------------ADD FIRST-----------------------//
    public void Addfirst(string data)
    {
        Node newNode = new Node(data);
        newNode.Next = head;

        if (head == null)
        {
            tail = newNode;
        }
        else
        {
            head.Prev = newNode;
        }

        head = newNode;
        Length++;
    }
    //-----------------------GET SPECIFIC NODE-----------------------//

    public string GetNth(int index)
    {
        Node current = head;
        //index of Node we are currently looking at - start at 1 bc i set txt file up to start at 1
        int count = 1;
        while (current != null)
        {
            if (count == index)
            {
                return current.Data;
            }

            count++;
            current = current.Next;
            //Debug.Log(current.Data);
        }
        return "0";
    }

    //-----------------------GET NEXT NODE-----------------------//
    public string GetNext(Node current)
    {
        Node followNode;
        if (active == null)
        {
            current = head;
        }
        else
        {
            followNode = active.Next;
            current = followNode;
        }
        active = current;
        return current.Data;
    }

}
