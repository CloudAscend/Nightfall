using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    private Rigidbody2D rigidbody;
    private Vector2 targetVec;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        target = GameManager.instance.playerController.transform;
    }

    private void Update()
    {
        targetVec.x = target.position.x - transform.position.x;
        targetVec.y = target.position.y - transform.position.y;

        targetVec = targetVec.normalized;

        rigidbody.velocity = targetVec * moveSpeed;

        Search();
    }

    private void Search()
    {
        Debug.DrawRay(transform.position, targetVec * Vector2.Distance(transform.position, target.position), Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetVec, Vector2.Distance(transform.position, target.position), LayerMask.GetMask("Obstacle"));

        if (hit)
        {
            
        }

        Debug.DrawRay(transform.position, targetVec * 2f, Color.blue);
        RaycastHit2D hitler = Physics2D.Raycast(transform.position, targetVec, 2f, LayerMask.GetMask("Obstacle"));
    }
}
