using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Dialogue : MonoBehaviour
{

    public DoubleLinkedList list = new DoubleLinkedList();

    public Dialogue(string path)
    {
   
    string[] lines;

        using (StreamReader sr = new StreamReader(path))
        {
            string input = sr.ReadToEnd();

            lines = input.Split('\n');
        }
        for (int i = 0; i < lines.Length; i++)
        {

            list.Add(lines[i]);
           // Debug.Log(list.Last.Data);
            Debug.Log(list.First);

        }
        
    }
        void Start()
        {

        }


        void Update()
        {

        }


    
}
