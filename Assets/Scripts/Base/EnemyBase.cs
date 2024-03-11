using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHp;
    public int hp;

    private void Awake()
    {
        hp = maxHp;
    }

    protected virtual void Update()
    {

    }

    protected virtual void Damage(int damage)
    {
        hp -= damage;
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //bullet
        {
            //Damage();
        }
    }
}
