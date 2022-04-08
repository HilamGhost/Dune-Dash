using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GridTypes 
{
    Empty,Exit,Start,Star,Obstacle
}
public class GridData : MonoBehaviour
{
    public int grid_id;
    public bool isPlayerInGrid;
    public bool isSelected;
    public int obs_id;

    public GridTypes gridType;
    
    [Header("Positions")]
    public int x_pos;
    public int z_pos;

    public Renderer Renderer 
    {
        get
        {
             return GetComponent<Renderer>();
        }
        
    }



    [Header("Visual")]
    public GameObject Obs_model;
    public GameObject StarModel;
    public ParticleSystem StarParticle;
    public ParticleSystem StarPickUpParticle;   
    public GameObject ExitGate;
    public GameObject SelectedObjectPS;

    [Header("Animators")]
    public Animator Obs_animator;
    public Animator star_animator;

    private void Update()
    {
        
        if (!isSelected) 
        {
            
            switch (gridType)
            {
                case GridTypes.Empty:
                    Renderer.material.color = new Color(0.8679245f, 0.5575108f, 0.3152367f, 1);
                    break;
                case GridTypes.Start:
                    Renderer.material.color = Color.green;
                    break;
                case GridTypes.Exit:
                    Renderer.material.color = Color.blue;
                    ExitGate.SetActive(true);
                    break;
                case GridTypes.Star:
                    Renderer.material.color = Color.yellow;
                    StarModel.SetActive(true);
                    break;
                case GridTypes.Obstacle:
                    GetComponent<Renderer>().material.color = Color.red;
                    Obs_model.SetActive(true);
                    break;
            }
        }
        else 
        {
            Renderer.material.color = new Color(0.3137255f, 0.8387181f, 0.8666667f,1);          
        }

        SelectedObjectPS.SetActive(isSelected);
    }

    private void OnMouseDown()
    {
        GridController.gridController.ClearAllSelected();
        
        isSelected = true;
        StartCoroutine(GameManager.gameManager.GridInfoUI(true, x_pos, z_pos, gridType,obs_id));
        
    }

    public IEnumerator OBSClose() 
    {
        Obs_animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(Obs_animator.GetCurrentAnimatorClipInfo(0).Length);
        ConsoleSystem.console.RunLine();
        Obs_model.SetActive(false);
        obs_id = 0;
    }
    public IEnumerator StarClose()
    {
        star_animator.SetTrigger("Destroy");
        StarParticle.Stop();
        StarPickUpParticle.Play();
        yield return new WaitForSeconds(1.5f);
        StarModel.SetActive(false);
    }
}
