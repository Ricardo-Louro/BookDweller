using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject                      player;
    [SerializeField] private float          moveSpeed;
    private Rigidbody2D                     rb;
    private Vector2                         direction;
    [SerializeField] private float          maxDistance;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject model;
    [SerializeField] private bool followPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = player.GetComponent<PlayerMovement>().GetLastDirection().normalized;
        model.transform.Rotate(new Vector3(0, 0,
            Vector2.SignedAngle(Vector2.left, direction)));
        
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(LifeTime());
    }

    private void Update()
    {
        //DestroyOnMaxDistance();
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
        if (followPlayer)
        {
            transform.position = player.transform.position;
            return;
        }
        rb.velocity = direction * (moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void DestroyOnMaxDistance()
    {
        if((player.transform.position - transform.position).magnitude >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
