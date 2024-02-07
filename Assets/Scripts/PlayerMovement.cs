using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Stats          stats;
    private Rigidbody2D                     rb;
    private Vector2                         moveDirection;
    private Vector2                         lastDirection;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        moveDirection = new Vector2();
        lastDirection = new Vector2(1,0);
    }

    // Update is called once per frame
    private void Update()
    {
        SetMoveDirection();
        UpdateLastDirection();
    }

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    private void SetMoveDirection()
    {
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxis("Horizontal");
    }

    private void Move(Vector2 moveDirection)
    {
        rb.velocity = moveDirection * (stats.MoveSpeed * Time.fixedDeltaTime);
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }

    private void UpdateLastDirection()
    {
        if(moveDirection != Vector2.zero)
        {
            lastDirection = moveDirection;
        }
    }
}
