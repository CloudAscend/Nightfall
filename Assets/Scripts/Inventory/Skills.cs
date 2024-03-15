using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private SkillBox boxPrefab;
    [SerializeField] private int boxCount;

    private void Start()
    {
        for (int i = 0; i < boxCount; i++)
        {
            Instantiate(boxPrefab, transform);
        }
    }
}
