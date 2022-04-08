using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    
    public static GridController gridController;
    [Header("Grids")]
    public List<GridData> Grids = new List<GridData>();

    [Header("Important Grids")]
    public Transform start_grid;
    public Transform exit_grid;
    public List<Transform> obs_grid;
    public List<Transform> star_grid;


    [HideInInspector] public  Transform first_grid;
    [HideInInspector] public  Transform last_grid;


    [Header("Player")]
    public GameObject player;

    [Header("Difficulty")]
    public int howManyStars;
    public int howManyObstacles;
    private void Awake()
    {
        gridController = this;
    }
    
    public void ChoosePlaces()
    {
        int i = Random.Range(0,Grids.Count);
        Grids[i].GetComponent<GridData>().gridType = GridTypes.Start;

        start_grid = Grids[i].transform;

        while(Grids[i].GetComponent<GridData>().gridType != GridTypes.Exit)
        {
            i = Random.Range(0, Grids.Count);
            if (Grids[i].GetComponent<GridData>().gridType == GridTypes.Empty)
            {
                Grids[i].GetComponent<GridData>().gridType = GridTypes.Exit;

                exit_grid = Grids[i].transform;
            }
        }
       
        for (int a = 0; a < howManyStars; a++)
        {
            i = Random.Range(0, Grids.Count);
            if (Grids[i].GetComponent<GridData>().gridType == GridTypes.Empty) 
            {
                Grids[i].GetComponent<GridData>().gridType = GridTypes.Star;

                star_grid.Add(Grids[i].transform);
                          
            }
            else 
            {
                a--;
            }
        }
        
        for (int a = 0; a < howManyObstacles; a++)
        {
            i = Random.Range(0, Grids.Count);
            if (Grids[i].GetComponent<GridData>().gridType == GridTypes.Empty)
            {
                Grids[i].GetComponent<GridData>().gridType = GridTypes.Obstacle;

                obs_grid.Add(Grids[i].transform);
                obs_grid[a].GetComponent<GridData>().obs_id = a;


            }
            else
            {
                a--;
            }
        }

        StartOver();
   
    }
    public void StartOver() 
    {
        player.transform.position = new Vector3(start_grid.transform.localPosition.x + 0.5f, player.transform.position.y, start_grid.transform.localPosition.z + 1);
        player.GetComponent<PlayerController>().targetPos = gridController.player.transform.position;
    }
    public void ClearAllSelected() 
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].GetComponent<GridData>().isSelected = false;
        }
    }
   
}
