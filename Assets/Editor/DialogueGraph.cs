using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//in order to make the graphview
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGV _graphView;
    private string _fileName = "Type File Name Here";

    //static method to open the Dilaogue Graph Window
    //to be able to call it statically from the editor
    //graph = main menu, dialogue graph = sub menu
    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        //giving the window a title
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    //when window is enabled
    private void OnEnable()
    {
        ConstructGV();
        CreateToolbar();
    }

    private void OnDisable()
    {
        //removing it so it doesnt overlap if it is opened multiple times
        rootVisualElement.Remove(_graphView);
    }

    private void ConstructGV()
    {
        //instance of dialogue gv
        _graphView = new DialogueGV
        {
            //name of this instance
            name = "DialogueGraph"
        };

        _graphView.StretchToParentSize();
        //adding graph window to unity work space window
        rootVisualElement.Add(_graphView);
    }

    //a toolbar tat will allow us to easily create a new node using a node creation button
    private void CreateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextF = new TextField("File Name: ");
        //set default val
        fileNameTextF.SetValueWithoutNotify(_fileName);
        //telling UI to repaint the visual element on the next frame
        fileNameTextF.MarkDirtyRepaint();
        //change callback listener
        fileNameTextF.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        //adding this text field to toolbar
        toolbar.Add(fileNameTextF);

        //new button to save data - { text = "Save Data"} sets text of buttn
        toolbar.Add(new Button(()=>RequestData(true)){ text = "Save Data"});
        //same for load
        toolbar.Add(new Button(()=>RequestData(false)) { text = "Load Data" });

        //adding a new button to the toolbar that allows the creation of a dialogue node
        var nodeCreateButton = new Button(()=> { _graphView.CreateNode("Dialogue Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        //add toolbar into editor window
        rootVisualElement.Add(toolbar);
    }

    private void RequestData(bool save)
    {
        //check if file is empty
        if (string.IsNullOrEmpty(_fileName))
        {
            //show error message
            EditorUtility.DisplayDialog("File name is invalid.", "Enter a valid file name.", "OK");
            return;
        }

        var saveUtil = GraphSaveLoad.GetInstance(_graphView);
        //if save then save graph otherwise load it
        if (save)
        {
            saveUtil.SaveGraph(_fileName);
        }
        else
        {
            saveUtil.LoadGraph(_fileName);
        }
    }
}
