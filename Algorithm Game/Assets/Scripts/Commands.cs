using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  In this code, we get the commands from this class
/// </summary>
public class Commands : MonoBehaviour
{
    public PlayerController player;
    int step_number;
    int obs_number;

   
    public void Command(string first_command,string second_command,string third_command) 
    {       
        
        switch (first_command) 
        {
            case "go":
                step_number = int.Parse(third_command);
                switch (second_command) 
                {
                    case "up":
                        StartCoroutine(player.MoveCommand(step_number,1,0));
                        break;
                    case "down":
                        StartCoroutine(player.MoveCommand(step_number, -1, 0));
                        break;
                    case "left":
                        StartCoroutine(player.MoveCommand(step_number, 0,-1));
                        break;
                    case "right":
                        StartCoroutine(player.MoveCommand(step_number, 0, 1));
                        break;
                    default:
                        Debug.LogError("WRONG");
                        break;
                }
            break;
            case "obs":
                obs_number = int.Parse(second_command);
                switch (third_command) 
                {
                    case "destroy":
                        GridController.gridController.obs_grid[obs_number].GetComponent<GridData>().gridType = GridTypes.Empty;
                        StartCoroutine(GridController.gridController.obs_grid[obs_number].GetComponent<GridData>().OBSClose());
                        GridController.gridController.obs_grid[obs_number] = null;
                        break;
                }

                break;
            default:
                Debug.LogError("WRONG");
                break;
        }
    }
}
