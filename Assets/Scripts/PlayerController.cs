using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float verticalInput;
    private float horizontalInput;
    private Vector2 moveDirection;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    private void Update()
    {
        ReceiveInputs();
        moveDirection = CalculateMoveDirection();
    }

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    private void ReceiveInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private Vector2 CalculateMoveDirection()
    {
        Vector2 moveDirection = new Vector2(0,1) * verticalInput + new Vector2(1,0) * horizontalInput;
        Debug.Log(moveDirection);
        return moveDirection.normalized;
    }

    private void Move(Vector2 moveDirection)
    {
        rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
