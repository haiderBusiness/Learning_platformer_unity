using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* [SerializedField] private float speed;  */
    private float speed = 10; 

    [SerializeField] private float jumpForce = 10;

    // when true the player is on the ground
    private bool grounded;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;

    private BoxCollider2D boxCollider;

    private float wallJumpCalldown;
    private float horizontalInput;


    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;


    // Awake is called once once the project starts (its a funciton in c#)
    private void Awake() 
    {
        // Grab references for the player's rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    

    // Update is called once per frame (its a funciton in c#)
    private void Update()
    {
        // Get the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Flip the player sprite based on the input
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        }
            
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
            
        


/*         if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
           Jump();
        } */


        // Set the "runing" animation based on the input:
        // if the arrow keys are pressed set to true else set to false
        anim.SetBool("run", horizontalInput != 0);

        // Set the "Grounded" animation based on the grounded variable
        anim.SetBool("grounded", grounded);
        /* anim.SetBool("grounded", isGrounded()); */

        




        // Log the note with the GameObject's name
        /* Debug.Log($"player is grounded: {isGrounded()}"); */


        // Wall jump logic
        if(wallJumpCalldown > 0.2f) {


            // Move the player at the speed of 10
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if(onWall() && !grounded) {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            } else 
                body.gravityScale = 7;

            // Jump the player on space press and if the player is grounded
            if(Input.GetKeyDown(KeyCode.Space)) {
                if(Input.GetKeyDown(KeyCode.Space) && grounded && jumpSound != null) {
                    SoundManager.instance.PlaySound(jumpSound);
                }
           
                Jump();

 
            }


        }
        else
            wallJumpCalldown += Time.deltaTime;
    
    }


    private void Jump() {

        if(grounded) {
            // Play the jump sound


            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            anim.SetTrigger("jump");

            // The player is no longer grounded
            grounded = false;
        } else if(onWall() && !grounded) {

            // push the play away from the wall
            if(horizontalInput == 0) {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = 
                new Vector3(
                    -Mathf.Sign(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z
                );
            } else 
                // make the player calimp up the wall
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCalldown = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Check if the player is grounded
        if(collision.gameObject.tag == "Ground") {
            // The player is grounded
            grounded = true;
        }
    }

    
    //TODO - Use the OnCollisionStay2D function to check if the player is on top of other objects

/*     void OnCollisionStay2D(Collision2D collision)
    {

        if (transform.position.y > collision.transform.position.y)
        {
            grounded = true;
        }
    } */



    //TODO - Use the isGrounded function
    private bool isGrounded() {
        // Check if the player is grounded
        RaycastHit2D raycastHit = 
        Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


      private bool onWall() {
        // Check if the player is grounded
        RaycastHit2D raycastHit = 
        Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size, 
            0, 
            new Vector2(transform.localScale.x, 0), 0.1f, 
            wallLayer
            );
        return raycastHit.collider != null;
    }



    public bool canAttack() {
        return !onWall();
    }
}
