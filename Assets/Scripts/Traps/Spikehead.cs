using UnityEngine;

public class Spikehead : EnemyDamage
{   
    private Vector3 destination;

    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;


    [Header("Sounds")]
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioClip hitPlayerSound;

    

    private Vector3 initialPosition;

    

    private float checkTimer; 



    private bool attacking; 


    private Vector3[] directions = new Vector3[4];



    private void OnEnable() {
        Stop();
    }


    private void Awake() {
        initialPosition = transform.position;
    }


    private void OnDisable() {
/*         transform.position = initialPosition; */
        /* transform.Translate(initialPosition * Time.deltaTime * speed);
        print("Spikehead disabled"); */
    }




    private void Update() {

        //Move the spikehead to the destination when attacking
        if(attacking) 
            transform.Translate(destination * Time.deltaTime * speed);
        else {

            // increas the check timer by delta time
            checkTimer += Time.deltaTime;

            if(checkTimer > checkDelay) {

                // check if player is in any of the directions near the Spikehead
                CheckForPlayer();
            }
        }

    }




    private void CheckForPlayer() {
        CalculateDirections();

        // check if the player is in any of the directions
        for(int i = 0; i < directions.Length; i++) {
            Debug.DrawRay(transform.position, directions[i], Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);


            // if the player is in the direction, set the destination to that direction and start attacking
            if(hit.collider != null && !attacking) {
                // start attacking
                attacking = true;
                destination = directions[i];
                // reset the check timer to 0
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections() {
        directions[0] = transform.right * range; // right direction 
        directions[1] = -transform.right * range; // left direction
        directions[2] = transform.up * range; // up direction
        directions[3] = -transform.up * range; // down direction;
    }


    private void Stop() {
        destination = transform.position; // stop the spikehead in the current position; 

        // stop attacking
        attacking = false; // stop attacking
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != ("Player")) {
            if(impactSound != null)
                SoundManager.instance.PlaySound(impactSound);
        } else {
            if(hitPlayerSound != null)
                SoundManager.instance.PlaySound(hitPlayerSound);
                /* collision.GetComponent<Health>().TakeDamage(damage); */
        }

        base.OnTriggerEnter2D(collision); // Excute logic from the parent class (EnemyDamage)

        // Stop spikehead from attacking when it hits the player
        Stop();
    }
}
