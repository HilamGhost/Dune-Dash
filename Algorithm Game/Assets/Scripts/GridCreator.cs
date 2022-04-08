using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [Header("Grid GameObjects")]
    public GameObject parent_gameObject;
    public GameObject grid_prefap;
    public GameObject last_grid;
    
    GameObject first_grid;
    GameObject last_grid_line;

    [Header("Valueables")]
    [SerializeField] Vector2 lastValues = new Vector2(9,-8);
    public float offset;

    [Header("Grid Datas")]
    int id;
    int x_pos;
    int z_pos;
    GridController gridController;
    // Start is called before the first frame update
    void Start()
    {
        gridController = GridController.gridController;
       
        first_grid = last_grid;
        last_grid_line = last_grid;       
        
        gridController.Grids.Add(last_grid.GetComponent<GridData>());
        


        CreateGrids();
        
        

        gridController.first_grid = first_grid.transform;
        gridController.last_grid = last_grid.transform;

        gridController.ChoosePlaces();    

    }

    void CreateGrids() 
    {
        CreateGridLine();
        while (last_grid_line.transform.position.z - offset >= lastValues.y)
        {            
            //Create new Grid Line 
            GameObject newLine = Instantiate(grid_prefap, new Vector3(last_grid_line.transform.position.x, last_grid_line.transform.position.y,last_grid_line.transform.position.z-offset), Quaternion.identity, parent_gameObject.transform);
            
            //Change values
            last_grid_line = newLine;
            id++;
            z_pos++;
            
            //Add the controller and define z coordinate
            gridController.Grids.Add(last_grid_line.GetComponent<GridData>());
            newLine.GetComponent<GridData>().grid_id = id;
            last_grid_line.GetComponent<GridData>().z_pos = z_pos;
            
            //Start Func.s
            CreateGridLine();
        }
        
    }
    void CreateGridLine() 
    {
        x_pos = 0;
        while (last_grid.transform.position.x + offset <= lastValues.x)
        {
            //Create new Grid
            GameObject newGrid = Instantiate(grid_prefap, new Vector3(last_grid.transform.position.x + offset, last_grid.transform.position.y, last_grid_line.transform.position.z), Quaternion.identity, parent_gameObject.transform);
            GridData newGridData = GetComponent<GridData>();
           
            //Define ID to Grid
            id++;
            newGridData.grid_id = id;
            
            //Define X and Z coordinates
            x_pos++;
            newGridData.x_pos = x_pos;
            newGridData.z_pos = z_pos;

            
            last_grid = newGrid;
            
            //Add to Grid Controller
            gridController.Grids.Add(last_grid.GetComponent<GridData>());


        }
        if(last_grid_line.transform.position.z - offset >= lastValues.y) last_grid = last_grid_line;
    }

  
}
