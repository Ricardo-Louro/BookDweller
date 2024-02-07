using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private float moveSpeed;
    
    
    [SerializeField] private int defaultHealthPoints = 1;
    [SerializeField] private float defaultMoveSpeed = 1;

    public int HP
    {
        get => healthPoints;
        set => healthPoints = value;
    }
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Start()
    {
        HP = defaultHealthPoints;
        MoveSpeed = defaultMoveSpeed;
    }
}
