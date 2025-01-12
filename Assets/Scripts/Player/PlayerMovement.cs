
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* [SerializedField] private float speed;  */
    private float speed = 10; 

    [Header("Jumping")]    
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityScale = 7;
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //Horizontal wall jump force
    [SerializeField] private float wallJumpY; //Vertical wall jump force





    [Header("Movement")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;



    [Header("Layers")]    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private bool grounded; // when true the player is on the ground


    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;



    private Rigidbody2D body;
    private Animator anim;

    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;


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
            transform.localScale = new Vector3(1.41f, 1.41f, 1.41f);
        }
            
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1.41f, 1.41f, 1.41f);
        }
            
        


        /*if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
           Jump();
        } */


        // Set the "runing" animation based on the input:
        // if the arrow keys are pressed set to true else set to false
        anim.SetBool("run", horizontalInput != 0);

        // Set the "Grounded" animation based on the grounded variable
     /*    anim.SetBool("grounded", grounded); */
        anim.SetBool("grounded", isGrounded());


        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }


        // Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }


        if(onWall()) {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        } else {
            body.gravityScale = gravityScale;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if(isGrounded()) {
                coyoteCounter = coyoteTime; // Reset coyote counter when on the ground;
                jumpCounter = extraJumps; // Reset the jump counter when on the ground
            } else {
                coyoteCounter -= Time.deltaTime; // Start decreasing coyote counter when not on the ground
            }
        }
    
    }


    private void Jump() {

        if(coyoteCounter < 0 && !onWall() && jumpCounter <= 0) return; // Exit the function if true

            // Play the jump sound
            if(jumpSound != null) {
                SoundManager.instance.PlaySound(jumpSound);
            }

            if(onWall()) {
                WallJump();
            } 
            else 
            {
                if(isGrounded()) 
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                else 
                {

                    // if not on the ground, and coyote counter is greater than 0, do a normal jump
/*                     if(coyoteCounter > 0) {
                        print("here coyote counter: ");
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                    } else 
                    {
                        if (jumpCounter > 0) { 
                            // if there are extra jumps available, do an extra jump 
                            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                            // Decrease the jump counter by 1
                            jumpCounter--;
                        }
                    } */


                        // coyote counter is not working only jumpcounter!
                        if (jumpCounter > 0) { 
                            // if there are extra jumps available, do an extra jump 
                            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                            // Decrease the jump counter by 1
                            jumpCounter--;
                        }
                    


                    // Reset coyote counter to 0 to avoid double jumps
                    coyoteCounter = 0;
                }
            }
    }


    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
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


      public bool onWall() {
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
/*         return !onWall(); */
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
