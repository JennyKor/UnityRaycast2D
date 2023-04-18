using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    bool isFacingLeft;

    bool isAgro = false;
    bool isSearching = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke(nameof(StopChasingPlayer), 5.0f);
                    // Debug.Log("Invoked!! Stopping the chase");
                }
            }
        }

        if (isAgro)
        {
            ChasePlayer();
        }
    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isFacingLeft)
        {
            castDist = -distance;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, castDist);
        
        if(hitInfo.collider != null)
        {
             Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(transform.position, hitInfo.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * castDist, Color.red);
        }
        return val;
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            isFacingLeft = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);     
    }

}
