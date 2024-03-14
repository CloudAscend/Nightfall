using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet; //Temp
    [SerializeField] private Transform shotPoint;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void FixedUpdate()
    {
        MousePoint();
    }

    private void MousePoint()
    {
        Vector2 mousePointer = gameManager.MousePos();

        float degree = Mathf.Atan2(mousePointer.y, mousePointer.x) * Mathf.Rad2Deg;

        shotPoint.rotation = Quaternion.Euler(0, 0, degree - 90f);
    }

    public void OnFire() //Left Mouse Buttons
    {
        PoolManager.instance.GetObject("PlayerBullet", shotPoint.position, shotPoint.rotation);
        //Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    }

    /*
    public Vector3 mousePos() //Temp
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        return point;
    }
    */
}
