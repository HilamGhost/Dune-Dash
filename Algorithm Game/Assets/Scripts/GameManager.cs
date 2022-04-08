using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public bool canMove;
    [Header("Other Scripts")]
    public PlayerController player;
    public GridController gridController = GridController.gridController;
    public ConsoleSystem consoleSystem = ConsoleSystem.console;
    public CameraController cameraController;

    [Header("Points")]
    public float Score;
    public int StarNumber;
    public int StepNumber;
    public float distance;

    [Header("GameOverScreens")]
    public GameObject gameOver_screen;
    public Text score_text;
    public Animator cameraAnim;
   
    [Header("Point UI")]
    public Text stars_text;
    public Text steps_text;

    [Header("Grid Info UI")]
    public bool grid_ui_is_open;
    public GameObject gridUI;
    public Text grid_pos_text;
    public Text grid_type_text;
    public Text grid_obstacle_id;
    public Text grid_obstace_title;
    private void Awake()
    {
        gameManager = this;
    }
    void Update()
    {
        stars_text.text = "Stars: "+ StarNumber;
        steps_text.text = "Steps: "+ StepNumber;
        canMove = !grid_ui_is_open;
    }
    public IEnumerator GoBack() 
    {
        cameraAnim.SetTrigger("Hurt");
        ConsoleSystem.console.CleanText();
        player.player_animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(2);
        gridController.StartOver();
       
    }
    public IEnumerator Win()
    {
        cameraController.GameWin();
        player.direction = new Vector2(0, -1);
        player.player_animator.SetTrigger("Win");
        distance = Vector3.Distance(gridController.start_grid.position,gridController.exit_grid.position);
        yield return new WaitForSeconds(2.6f);
        Score = Mathf.Round((distance * (StarNumber+1)) / StepNumber);
        gameOver_screen.SetActive(true);
        score_text.text = "Your score is: " + Score;
    }

    public IEnumerator GridInfoUI(bool isOpen, int grid_x, int grid_z, GridTypes grid,int obsID)
    {
        
        if (isOpen) 
        {
            gridUI.SetActive(isOpen);
            grid_ui_is_open = isOpen;
            grid_pos_text.text = "(" + grid_x + "," + grid_z + ")";
            grid_type_text.text = grid.ToString();
            if(grid == GridTypes.Obstacle) 
            {
                grid_obstace_title.enabled = true;
                grid_obstacle_id.text = obsID.ToString();
            }
            else 
            {
                grid_obstace_title.enabled = false;
                grid_obstacle_id.text = "";
            }
        }
        else 
        {
            GridController.gridController.ClearAllSelected();
            grid_ui_is_open = isOpen;
            
            gridUI.GetComponent<Animator>().SetTrigger("Close");
            yield return new WaitForSeconds(1);
            gridUI.SetActive(isOpen);
            
            grid_pos_text.text = null;
            grid_type_text.text = null;


        }
    }
    public void CloseInfoUI() 
    {
        StartCoroutine(GridInfoUI(false,0,0,GridTypes.Empty,0));
    }
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }
}
