using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    [Tooltip("OFFSET ÝLE AYNI OLMALI")] public float move_speed = 2.25f;
    public float SmoothAmount = 0.1f;
    [Space]

    [Header("Player Datas")]
    public int PlayerGridID;
    public  Vector3 targetPos;
    public bool canMove = true;
    GridController GridManager { get {return GridController.gridController;} }
    public Vector2 direction;

    [Header("Visual")]
    public Animator player_animator;
    public ParticleSystem step_particle;

    void Update()
    {
        canMove = GameManager.gameManager.canMove;

        if (targetPos != transform.position) 
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, SmoothAmount*Time.deltaTime);         
        }
        else 
        {
            transform.position = targetPos;
        }
        DirectionChange();
        
    }
    private void Move(int x, int z)
    {
        targetPos = transform.position + new Vector3(x * move_speed, 0, z * move_speed);
        player_animator.SetTrigger("Run");
        step_particle.Play();
    }
    public IEnumerator MoveCommand(int step_number, int x, int z) 
    {

        GameManager.gameManager.canMove = false;
        switch (x, z)
        {
            case (1,0): //UP
                for (int i = 0; i < step_number; i++)
                {
                   
                    if (transform.localPosition.z + move_speed <= GridManager.first_grid.localPosition.z + 1) Move(0, 1); else break;
                    direction = new Vector2(0,1);
                    yield return new WaitForSeconds(1);
                }
                
                yield return new WaitForSeconds(1);
                ConsoleSystem.console.RunLine();
                break;
            case (-1, 0): //DOWN
                for (int i = 0; i < step_number; i++)
                {
                    
                    if (transform.localPosition.z - move_speed >= GridManager.last_grid.localPosition.z - 1) Move(0, -1); else break;
                    direction = new Vector2(0, -1);
                    yield return new WaitForSeconds(1);         
                }
                
                yield return new WaitForSeconds(1);
                ConsoleSystem.console.RunLine();
                break;
            case (0, -1): //LEFT
                for (int i = 0; i < step_number; i++)
                {                    
                    if (transform.localPosition.x - move_speed >= GridManager.first_grid.localPosition.x - 0.5f) Move(-1, 0); else break;
                    direction = new Vector2(-1, 0);
                    yield return new WaitForSeconds(1);             
                }
                
                yield return new WaitForSeconds(1);
                ConsoleSystem.console.RunLine();
                break;
            case (0, 1): //RIGHT
                for (int i = 0; i < step_number; i++)
                {                 
                    if (transform.localPosition.x + move_speed <= GridManager.last_grid.localPosition.x + 0.5f) Move(1, 0); else break;
                    direction = new Vector2(1, 0);
                    yield return new WaitForSeconds(1);                                               
                }
                
                yield return new WaitForSeconds(1);
                ConsoleSystem.console.RunLine();
                break;
        }
    }
    void DirectionChange() 
    {
        if (direction.x > 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), SmoothAmount * Time.deltaTime);
        if (direction.x < 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), SmoothAmount * Time.deltaTime);
        if (direction.y > 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), SmoothAmount * Time.deltaTime);
        if (direction.y < 0) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), SmoothAmount * Time.deltaTime);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Grid")
        {
            GridData gridData = other.transform.GetComponent<GridData>();
            PlayerGridID = other.transform.GetComponent<GridData>().grid_id;
            
            if (!other.transform.GetComponent<GridData>().isPlayerInGrid) 
            {
                if (gridData.gridType == GridTypes.Exit) 
                {
                    gridData.isPlayerInGrid = true;
                    
                    Debug.Log("WIN");
                    
                    StartCoroutine(GameManager.gameManager.Win());
                } 
                if (gridData.gridType == GridTypes.Obstacle) 
                {
                    Debug.LogError("GameOver");
                    
                    gridData.isPlayerInGrid = true;
                    
                    StartCoroutine(GameManager.gameManager.GoBack());
                }
                if (gridData.gridType == GridTypes.Star) 
                {

                    gridData.isPlayerInGrid = true;
                   
                    GameManager.gameManager.StarNumber++;

                    gridData.gridType = GridTypes.Empty;
                    
                    StartCoroutine(other.transform.GetComponent<GridData>().StarClose());
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Grid")
        {
            other.transform.GetComponent<GridData>().isPlayerInGrid = false;

        }      
    }
}
