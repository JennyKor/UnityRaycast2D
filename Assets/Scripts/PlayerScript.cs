using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    BoxCollider2D boxcollider;

    //[SerializeField]
    //private LayerMask groundLayerMask;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    public const string RIGHT = "right";
    public const string LEFT = "left";
    public const string JUMP = "jump";

    string buttonPressed;

    bool faceRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();

        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            buttonPressed = RIGHT;
           
            if (!faceRight)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            buttonPressed = LEFT;
            if (faceRight)
            {
                Flip();
            }
        }
        else
        {
            buttonPressed = null;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            buttonPressed = JUMP;
        }
    }

    private void FixedUpdate()
    {
        if (buttonPressed == RIGHT)
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
        else if (buttonPressed == LEFT)
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);

        }

        if (IsGrounded() && buttonPressed == JUMP)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        // RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, boxcollider.bounds.extents.y + 0.2f, groundLayerMask);

        RaycastHit2D raycastHit = Physics2D.Raycast(boxcollider.bounds.center, Vector2.down, boxcollider.bounds.extents.y + 0.2f);

        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.white;
        }
        else
        {
            rayColor = Color.blue;
        }
        //Debug.DrawRay(transform.position, Vector2.down * (boxcollider.bounds.extents.y + 0.2f), rayColor);

        Debug.DrawRay(boxcollider.bounds.center, Vector2.down * (boxcollider.bounds.extents.y + 0.2f), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180, 0f);
    }
}
