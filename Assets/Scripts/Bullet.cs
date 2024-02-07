using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject                      player;
    [SerializeField] private float          moveSpeed;
    private Rigidbody2D                     rb;
    private Vector2                         direction;
    [SerializeField] private float          maxDistance;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = player.GetComponent<PlayerMovement>().GetLastDirection().normalized;
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        DestroyOnMaxDistance();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rb is not null)
        {
            Move();
        }   
    }

    private void Move()
    {
        rb.velocity = direction * (moveSpeed * Time.fixedDeltaTime);
    }

    private void DestroyOnMaxDistance()
    {
        if((player.transform.position - transform.position).magnitude >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
