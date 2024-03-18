using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    public Transform target;
    private Vector2 targetPos;

    public float moveSpeed;
    private Vector2 moveVec;

    public float cooltime;

    private Rigidbody2D rigidbody;
    private float timeRate;
    private bool isWall;

    private AStar astar;
    private List<Vector2> vecList = new List<Vector2>();
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        astar = AStar.instance;
    }

    private void Update()
    {
        Debug.Log(isWall);

        if (timeRate > cooltime)
        {
            timeRate -= cooltime;
            Search();
        }
        else
        {
            timeRate += Time.deltaTime;
        }
        PathCross();
        Move();
    }

    private void Move()
    {
        moveVec.x = targetPos.x - transform.position.x;
        moveVec.y = targetPos.y - transform.position.y;

        rigidbody.velocity = moveVec.normalized * moveSpeed;
    }

    private void PathCross()
    {
        if (isWall && vecList.Count > 0)
        {
            targetPos.x = vecList[0].x;
            targetPos.y = vecList[0].y;

            if (Vector2.Distance(targetPos, vecList[0]) < 0.3f)
                vecList.Remove(vecList[0]);
        }
    }

    private void Search()
    {
        //A*
        targetPos = target.position;

        if (isWall)
        {
            astar.PathFinding(transform.position, target.position);

            vecList.Clear();

            //for (int i = 0; i < astar.FinalNodeList.Count; i++)
            //{
            //    vecList.Add(new Vector2(astar.FinalNodeList[i].x, astar.FinalNodeList[i].y));
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) isWall = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) isWall = false;
    }
}
