using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleSystem : MonoBehaviour
{
    public static ConsoleSystem console;

    Commands commands;
    public InputField console_input;
    public Button apply_button;

    public string input = "Mercan_Mert";
    [Header("Line Input")]
    public List<string> splited_line = new List<string>();
    public char line_prefix = '&';
    

    [Header("Code Input")] 
    public List<string> splited_code = new List<string>();
    public char code_prefix = '_';

    private void Awake()
    {
        console = this;
    }

    void Start()
    {
        commands = GetComponent<Commands>();
    }

    void Update()
    {
         console_input.interactable= commands.player.canMove;
         apply_button.interactable = commands.player.canMove;
    }
    public void ApplyText() 
    {
        CleanText();
        input = console_input.text;
        console_input.text = null;
        SplitLine();

        GameManager.gameManager.StepNumber +=splited_line.Count;

        RunLine();
        

    }
    public void RunLine() 
    {
        if (splited_line.Count > 0)
        {
            RunCommand();
            DeleteLine();
        }
        else 
        {
            commands.player.canMove = true;
        }
        
        
    }
    void RunCommand() 
    {   
        SplitCode(splited_line[0]);   
        commands.Command(splited_code[0],splited_code[1],splited_code[2]);
        
    }

    void SplitLine() 
    {
        string[] a = input.Split(line_prefix);
        for (int i = 0; i < a.Length; i++)
        {
            splited_line.Add(a[i]);
        }
    }
    void DeleteLine() 
    {
        splited_line.Remove(splited_line[0]);
        splited_code.Clear();

    }
    void SplitCode(string codes) 
    {
        string[] a = codes.Split(code_prefix);
        for (int i = 0; i < a.Length; i++)
        {
            splited_code.Add(a[i]);
        }
    } 
    public void CleanText() 
    {
        input = null;
        splited_line.Clear();
        splited_code.Clear();
        
    }
}
