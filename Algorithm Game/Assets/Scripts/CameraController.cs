using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Coordinates")]
    public Vector3 gameEndPos;
    Vector3 target;
    public GameObject player;
    [Header("Smooth")]
    public float smoothAmount;

    void Update()
    {
        if (target == gameEndPos) 
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, smoothAmount*Time.deltaTime);
        }
    }
    public void GameWin() 
    {
        transform.parent = player.transform;
        target = gameEndPos;
    }
}
