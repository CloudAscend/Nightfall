using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserTest : MonoBehaviour
{
    private GameManager gameManager;

    private Vector3 curserPos;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {
        MoveCurser();
    }

    private void MoveCurser()
    {
        curserPos.x = Mathf.FloorToInt(gameManager.MousePos().x);
        curserPos.y = Mathf.FloorToInt(gameManager.MousePos().y);

        transform.position = curserPos;
    }
}
