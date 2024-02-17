using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private float catchupDistance;
    [SerializeField] private float catchupSpeed;
    private float moveSpeed;

    private Transform playerTransform;
    private Rigidbody2D _rigidbody;

    private float playerDistance = 0;
    private Vector2 _moveDirection;
    private float _defaultXScale;
    private Vector2 walkDirectionReference;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
        _defaultXScale = transform.localScale.x;
        walkDirectionReference = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveDirection();
        SetFacingDirection();
        SetSpeed();
        UpdatePosition();
    }

    private void SetMoveDirection()
    {
        if (playerTransform is not null)
        {
            walkDirectionReference = playerTransform.position;
        }

        _moveDirection.x = walkDirectionReference.x - transform.position.x;
        _moveDirection.y = walkDirectionReference.y - transform.position.y;
    }

    private void SetFacingDirection()
    {
        if (walkDirectionReference.x < transform.position.x)
            transform.localScale = new Vector3(_defaultXScale,
                transform.localScale.y, transform.localScale.z);
        else
        {
            transform.localScale = new Vector3(-_defaultXScale,
                transform.localScale.y, transform.localScale.z);
        }
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
        _rigidbody.velocity = _moveDirection.normalized * (moveSpeed * Time.fixedDeltaTime);
    }
}
