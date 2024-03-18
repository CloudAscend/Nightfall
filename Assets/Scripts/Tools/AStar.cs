using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public bool Obstacle;
    public Node ParentNode;
    public int x, y, G, H;
    public int F { get { return G + H; } }

    public Node(bool isWall, int _x, int _y)
    {
        Obstacle = isWall;
        x = _x;
        y = _y;
    }
}

public class AStar : MonoBehaviour
{
    public static AStar instance;

    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PathFinding(new Vector2(-7, 0), new Vector2(7, 0));
    }

    public void PathFinding(Vector2 start, Vector2 target)
    {
        startPos.x = Mathf.FloorToInt(start.x);
        startPos.y = Mathf.FloorToInt(start.y);

        targetPos.x = Mathf.FloorToInt(target.x);
        targetPos.y = Mathf.FloorToInt(target.y);

        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D collider in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                        isWall = true;
                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }

        //HIL
        bottomLeft.x = 0;
        bottomLeft.y = 0;
        topRight.x = sizeX / 2 - 1;
        topRight.y = sizeY / 2 - 1;
        startPos.x += sizeX / 2;
        startPos.y += sizeY / 2;

        Debug.Log("Why the fuck");

        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        while (OpenList.Count > 0) //OpenList내 데이터가 없을 때까지 반복문
        {
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return;
            }

            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    private void OpenListAdd(int X, int Y) //Read It!
    {
        if (X >= bottomLeft.x && X < topRight.x + 1 &&
            Y >= bottomLeft.y && Y < topRight.y + 1 &&
!NodeArray[X - bottomLeft.x, Y - bottomLeft.y].Obstacle &&
!ClosedList.Contains(NodeArray[X - bottomLeft.x, Y - bottomLeft.y]))
        {
            if (allowDiagonal)
                if (NodeArray[CurNode.x - bottomLeft.x, Y - bottomLeft.y].Obstacle &&
                    NodeArray[X - bottomLeft.x, CurNode.y - bottomLeft.y].Obstacle)
                    return;

            if (dontCrossCorner)
                if (NodeArray[CurNode.x - bottomLeft.x, Y - bottomLeft.y].Obstacle ||
                    NodeArray[X - bottomLeft.x, CurNode.y - bottomLeft.y].Obstacle)
                    return;

            Node NeighborNode = NodeArray[X, Y];
            int MoveCost = CurNode.G + (CurNode.x - X == 0 || CurNode.y - Y == 0 ? 10 : 14);

            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x)
                    + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0)
            for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y),
                    new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}
