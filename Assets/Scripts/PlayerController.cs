using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float          moveSpeed;
    private Rigidbody2D                     rb;
    private Vector2                         moveDirection;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        moveDirection = new Vector2(); 
    }

    // Update is called once per frame
    private void Update()
    {
        SetMoveDirection();
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
        rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
