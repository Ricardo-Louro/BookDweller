using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private float catchupDistance;
    [SerializeField] private float catchupSpeed;
    private float moveSpeed;

    [SerializeField] private Transform playerTransform;
    private Rigidbody2D _rigidbody;

    private float playerDistance = 0;
    private Vector2 _moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveDirection();
        SetSpeed();
        UpdatePosition();
    }

    private void SetMoveDirection()
    {
        if (playerTransform is null) 
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        _moveDirection.x = playerTransform.position.x - transform.position.x;
        _moveDirection.y = playerTransform.position.y - transform.position.y;
    }

    private void SetSpeed()
    {
        playerDistance = _moveDirection.magnitude;

        if(playerDistance >= catchupDistance && moveSpeed != catchupSpeed)
        {
            moveSpeed = catchupSpeed;
        }
        else if(playerDistance <= catchupDistance && moveSpeed != stats.MoveSpeed)
        {
            moveSpeed = stats.MoveSpeed;
        }
    }

    private void UpdatePosition()
    {
        _rigidbody.velocity = _moveDirection.normalized * (moveSpeed * Time.deltaTime);
    }
}
