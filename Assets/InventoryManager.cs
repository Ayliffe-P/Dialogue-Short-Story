using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject drink;
    public GameObject spawndDrink;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnDrink() {

        if (spawndDrink == null)
        {
            spawndDrink = Instantiate(drink);
        }
        
    
    }

    public void takeDrink()
    {

        if (spawndDrink != null)
        {
            Destroy(spawndDrink);
        }


    }
}
