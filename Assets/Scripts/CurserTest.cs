using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CurserTest : MonoBehaviour //This Class is Parent
{
    private GameManager gameManager;

    private PlayerInput playerInput;
    private InputAction fireAction;

    private Vector3 curserPos;

    private bool isMove = true;

    private SpriteRenderer spriteRend; //Temp

    public string tag; //Temp

    //Trap

    private void Start()
    {
        gameManager = GameManager.instance;

        playerInput = gameManager.playerController.playerInput;
        fireAction = playerInput.actions.FindAction("Fire");

        spriteRend = GetComponent<SpriteRenderer>(); //Temp
    }

    private void Update()
    {
        if (isMove)
            MoveCurser();
    }

    private void MoveCurser()
    {
        curserPos.x = Mathf.FloorToInt(gameManager.MousePos().x);
        curserPos.y = Mathf.FloorToInt(gameManager.MousePos().y);

        transform.position = curserPos;

        if (fireAction.triggered) //Temp(BoxCast를 넣어 isWall인지 확인 후 가능하면 작동)
        {
            isMove = false;
            spriteRend.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Temp
    {
        if (other.gameObject.CompareTag("Enemy") && !isMove)
        {
            //gameObject.GetComponent<EnemyBase>().Damage();
            PoolManager.instance.PoolObject(tag, gameObject);
        }
    }
}
