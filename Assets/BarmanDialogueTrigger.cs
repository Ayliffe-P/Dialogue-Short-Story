﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarmanDialogueTrigger : NewDialogueTrigger
{
    private InventoryManager invMan;

    void Start()
    {
        invMan = GameObject.Find("Manager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NewDialogueManager.DLM.spawnDrink)
        {
            invMan.spawnDrink();
            Debug.Log("called Spawn");
        }
    }
}
