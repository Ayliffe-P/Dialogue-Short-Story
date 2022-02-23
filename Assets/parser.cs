using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
public class parser : MonoBehaviour
{
    public static parser _p;
    public static parser P { get { return _p; } }

    Hashtable commands = new Hashtable();
    public Hashtable inv = new Hashtable();

    string[] interpret;

    public GameObject inputTxt;
    public GameObject inputField;
    public GameObject outputText;
    public GameObject player;
    public GameObject shane;
    public InventoryManager invM;
    public GameObject barman;
    string key;
    string value;
    public PostProcessProfile profile;
   

    private void Awake()
    {
        if (_p != null && _p != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _p = this;
        }

        //blank key to give access to any dialoge that has no key
        inv.Add(000, "");
        invM = GameObject.Find("Manager").GetComponent<InventoryManager>();
        player = GameObject.Find("Player");
        shane = GameObject.Find("Shane");
        barman = GameObject.Find("Barman");
        profile = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile;
        // pickUp = GetComponent<AudioSource>();
        
    }

    void StoreInput()
    {
        //storing the input text into the array and splitting it by using space
        interpret = inputTxt.GetComponent<Text>().text.Split(' ');
    }
    void Start()
    {
        AddCommmands();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StoreInput();
            TextOutput();
        }
        else if (Input.GetMouseButton(0))
        {
            inputTxt.SetActive(true);
            outputText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputField.active == true)
            {
                inputField.SetActive(false);

                //Arnas.GetComponent<NewDialogueTrigger>().enabled = true;
                //Depression.GetComponent<NewDialogueTrigger>().enabled = true;
                //Player.GetComponent<MonologueTrigger>().enabled = true;
            }
            else if (inputField.active == false)
            {
                inputField.SetActive(true);

                //Arnas.GetComponent<NewDialogueTrigger>().enabled = false;
                //Depression.GetComponent<NewDialogueTrigger>().enabled = false;
                //Player.GetComponent<MonologueTrigger>().enabled = false;
            }
        }
    }
    void ToggleUI()
    {
        inputTxt.SetActive(false);
        outputText.SetActive(true);
    }
    void TextOutput()
    {

        if (interpret[0] == "pick" && interpret[1] == "up")
        {
            key = null;
            value = null;
            //string array into a single string
            foreach (string item in interpret)
            {
                key += item + " ";
            }

            if (key == "pick up drink ")
            {
                if (invM.drink != null && Vector3.Distance(invM.drink.transform.position, player.transform.position) < 3)
                {
                    ToggleUI();
                    value = commands[key].ToString();
                    inv.Add(001, "drink");
                    outputText.GetComponent<Text>().text = value;
                    GetComponent<InventoryManager>().takeDrink();
                }
                else
                {
                    ToggleUI();
                    outputText.GetComponent<Text>().text = "I cant do that";
                }


            }
        }
        else if (interpret[0] == "walk" && interpret[1] == "to")
        {
            key = null;
            value = null;
            //string array into a single string
            foreach (string item in interpret)
            {
                key += item + " ";
            }

            if (key == "walk to barman ")
            {
                // Vector3 temp = new Vector3(1,0,0);
                player.GetComponent<NavMeshAgent>().SetDestination(barman.transform.GetChild(0).position);
            } else if (key == "walk to shane ")
            {
                player.GetComponent<NavMeshAgent>().SetDestination(shane.transform.position);

            }

        }

        else if (interpret[0] == "use")
        {
            key = null;
            value = null;
            //string array into a single string
            foreach (string item in interpret)
            {
                key += item + " ";
            }

            if (key == "use drink " && inv.ContainsKey(001))
            {
                Debug.Log("drinking ");
                profile.GetSetting<ChromaticAberration>().intensity.Override(1f);
            }
            else
            {
                ToggleUI();
                outputText.GetComponent<Text>().text = "I cant do that";
            }
            
        }
        if (interpret[0] == "talk" && interpret[1] == "to")
        {
            key = null;
            value = null;
            //string array into a single string
            foreach (string item in interpret)
            {
                key += item + " ";
            }
            if (key == "talk to shane " && Vector3.Distance(shane.transform.position, player.transform.position) < 3)
            {
                shane.GetComponent<NewDialogueTrigger>().start();
            }
            if (key == "talk to barman " && Vector3.Distance(barman.transform.position, player.transform.position) < 3)
            {
                barman.GetComponent<NewDialogueTrigger>().start();
            }
            else
            {
                ToggleUI();
                outputText.GetComponent<Text>().text = "They're not around";
            }

        }
        }

    void AddCommmands()
    {
        //LOOK AT
        commands.Add("pick up drink ", "A drink has been added");
        commands.Add("use drink ", "You took a sip of your drink");
    }
    }
