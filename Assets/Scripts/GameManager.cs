using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Objects")]
    public PlayerController playerController;

    [Header("Tools")]
    public AStar aStar;

    private AStar curAStar; //Temp

    private void Awake()
    {
        instance = this;

        curAStar = GetComponent<AStar>();
        aStar = curAStar;
    }

    public Vector3 MousePos() //Temp
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        return point;
    }
}
