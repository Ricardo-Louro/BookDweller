using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Stats          stats;
    [SerializeField] private GameObject     model;
    private Rigidbody2D                     rb;
    private Vector2                         moveDirection;
    private Vector2                         lastDirection;
    private float initialXScale;
    private Animator animator;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        moveDirection = new Vector2();
        lastDirection = new Vector2(1,0);
        initialXScale = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetMoveDirection();
        SetFacingDirection();
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

    private void SetFacingDirection()
    {
        transform.localScale = moveDirection.x switch
        {
            > 0 => new Vector3(initialXScale, model.transform.localScale.y,
                model.transform.localScale.z),
            < 0 => new Vector3(-initialXScale, model.transform.localScale.y,
                model.transform.localScale.z),
            _ => model.transform.localScale
        };
    }

    private void Move(Vector2 moveDirection)
    {
        rb.velocity = moveDirection * (stats.MoveSpeed * Time.fixedDeltaTime);
        
        model.GetComponent<Animator>().SetBool("Walking", this.moveDirection.magnitude > 0);
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
