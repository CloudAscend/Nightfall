using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public string tag;

    [SerializeField] private string damageTag; //Temp
    private void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OutMap"))  
            OutMap();
    }

    private void OutMap()
    {
        PoolManager.instance.PoolObject(tag, gameObject);
    }
}
