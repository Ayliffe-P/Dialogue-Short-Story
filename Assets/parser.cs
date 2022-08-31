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

    string[] inptCommand;

    public GameObject inputText;
    public GameObject inputField;
    public GameObject outputText;
    public GameObject player;
    public GameObject shane;
    public InventoryManager invM;
    public GameObject barman;
    string key;
    string value;
    public PostProcessProfile profile;

    public AudioSource audio;
    public AudioClip gulp;
    public AudioClip pickup;

   


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

      
        inv.Add(000, "");
        invM = GameObject.Find("Manager").GetComponent<InventoryManager>();
        player = GameObject.Find("Player");
        shane = GameObject.Find("Shane");
        barman = GameObject.Find("Barman");
        profile = GameObject.Find("PostProcessing").GetComponent<PostProcessVolume>().profile;
       
    }

    void StoreInput()
    {
      
        inptCommand = inputText.GetComponent<Text>().text.Split(' ');
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
            inputText.SetActive(true);
            outputText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputField.active == true)
            {
                inputField.SetActive(false);

              
            }
            else if (inputField.active == false)
            {
                inputField.SetActive(true);

               
            }
        }
    }
    void ToggleUI()
    {
        inputText.SetActive(false);
        outputText.SetActive(true);
    }
    void TextOutput()
    {

        if (inptCommand[0] == "pick" && inptCommand[1] == "up")
        {
            key = null;
            value = null;
          
            foreach (string item in inptCommand)
            {
                key += item + " ";
            }

            if (key == "pick up drink ")
            {
                if (invM.drink != null && Vector3.Distance(invM.drink.transform.position, player.transform.position) < 3)
                {
                    audio.PlayOneShot(pickup, 0.5f);
                    ToggleUI();
                    value = commands[key].ToString();
                    if (!inv.Contains(001))
                    {
                        inv.Add(001, "drink");
                    }
                    
                    outputText.GetComponent<Text>().text = value;
                    GetComponent<InventoryManager>().takeDrink();
                    ObjectiveManager._OBJMAN.tasks.Remove("Get a drink");
                    if (!ObjectiveManager._OBJMAN.contains("Talk to Shane"))
                    {
                        ObjectiveManager._OBJMAN.tasks.Add("Talk to Shane");
                    }
                }
                else
                {
                    ToggleUI();
                    outputText.GetComponent<Text>().text = "I cant do that";
                }


            }
        }
        else if (inptCommand[0] == "walk" && inptCommand[1] == "to")
        {
            key = null;
            value = null;
          
            foreach (string item in inptCommand)
            {
                key += item + " ";
            }

            if (key == "walk to barman ")
            {
             
                player.GetComponent<NavMeshAgent>().SetDestination(barman.transform.GetChild(0).position);
            } else if (key == "walk to shane ")
            {
                player.GetComponent<NavMeshAgent>().SetDestination(shane.transform.position);

            }

        }

        else if (inptCommand[0] == "use")
        {
            key = null;
            value = null;
        
            foreach (string item in inptCommand)
            {
                key += item + " ";
            }

            if (key == "use drink " && inv.ContainsKey(001) && ObjectiveManager._OBJMAN.contains("Take a drink"))
            {
                Debug.Log("drinking ");
                audio.PlayOneShot(gulp, 0.5f);
                profile.GetSetting<ChromaticAberration>().intensity.Override(1f);
            
                ObjectiveManager._OBJMAN.tasks.Remove("Take a drink");
                ObjectiveManager._OBJMAN.tasks.Add("Go talk to the Barman");
                barman.GetComponent<GraphDialogueTrigger>().dialogue = Resources.Load<ScriptableDialogue>("barmanDialogue2");
            }
            else if (key == "use drink " && inv.ContainsKey(001)) {
                ToggleUI();
                outputText.GetComponent<Text>().text = "We should save it for later";
            }
            
            else
            {
                ToggleUI();
                outputText.GetComponent<Text>().text = "I cant do that";
            }
            
        }
        if (inptCommand[0] == "talk" && inptCommand[1] == "to")
        {
            key = null;
            value = null;
        
            foreach (string item in inptCommand)
            {
                key += item + " ";
            }
            if (key == "talk to shane " && Vector3.Distance(shane.transform.position, player.transform.position) < 3)
            {
                shane.GetComponent<GraphDialogueTrigger>().start();
            }
            if (key == "talk to barman " && Vector3.Distance(barman.transform.position, player.transform.position) < 3)
            {
                barman.GetComponent<GraphDialogueTrigger>().start();
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
      
        commands.Add("pick up drink ", "A drink has been added");
        commands.Add("use drink ", "You took a sip of your drink");
    }
    }
