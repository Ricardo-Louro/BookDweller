using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Transform playerTransform;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (playerTransform is null) _rigidbody.velocity = Vector2.zero;
        else
        {
            _moveDirection = playerTransform.position - transform.position;
            _moveDirection = _moveDirection.normalized;
            _rigidbody.velocity = _moveDirection * moveSpeed/ 100 / Time.deltaTime;
        }
    }
}
