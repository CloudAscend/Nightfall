using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public int maxHP;
    public int HP;

    public Transform target;
    public float moveSpeed;

    private Rigidbody2D rigidbody;
    private Vector2 targetVec;

    private PoolManager poolManager;
    public string tag;

    private void Awake() //자기 자신한테 적용
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() //남의 것에 적용
    {
        target = GameManager.instance.playerController.transform;
        poolManager = PoolManager.instance;

        HP = maxHP;
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

    //Temp {
    public void Damage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            Death();
        }
    }

    private void Death()
    {
        poolManager.PoolObject(tag, gameObject);
    }
    // }
}
