using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; 
    [SerializeField] private int damage; 
    [SerializeField] private float range; // Range: the distance where the enemy can see the player and attack 


    
    [Header("Ranged Attack")] 

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private AudioClip fireballSound;





    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;




    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity; 


    private Animator anim; 
    private EnemyPatrol enemyPatrol;


    private Rigidbody2D rb;



    void OnEnable()
    {
        if (rb != null)
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (boxCollider != null)
            boxCollider.enabled = true; // Disable the Collider component
    }

    private void OnDisable() {
/*         print("Disabled"); */
        if (boxCollider != null)
            boxCollider.enabled = false; // Disable the Collider component

        if (rb != null)
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        /* boxCollider.enabled = false; */
    }



    
    private void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();


    }


        private void Update() {
        cooldownTimer += Time.deltaTime; 

        // Attack only when player in sight
        if(PlayerInSight()) {
                if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }


        if(enemyPatrol != null) {
            enemyPatrol.enabled = !PlayerInSight();
        } else {
            print(
                "Enemy Patrol is null, assign the Enemy Patrol script to the enemy if you want the enemy to move, this message is from RangedEnemy.cs");
        }

    }




    private bool PlayerInSight() {

        ///READ THIS: 
        
        // boxCollider.bounds.center: is the center of the box collider
        // transform.right * range: is the direction where the enemy can see the player
        // transform.localScale.x: is the direction where the enemy is facing
        // boxCollider.bounds.size: is the size of the box collider
        // Vector2.left: is the direction where the enemy can see the player
        // 0: is the angle of the box cast
        // playerLayer: is the layer where the player is in


        // To get the boxcollider rotate with the enemy, we need to add transform.right * range 
        // to the center of the box collider multiplied by the scale of the enemy 
        // to get the direction where the enemy is facing

        //
        

        // To increase the size of the box collider, we can to multiply the size of the box collider 
        // by the range (the float number that we set)
        


        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


            
        return hit.collider != null;
    }


    private void RangedAttack() {
        cooldownTimer = 0;

        if(fireballSound != null) 
            SoundManager.instance.PlaySound(fireballSound);

        fireballs[FindFireBall()].transform.position = firepoint.position;
        fireballs[FindFireBall()].GetComponent<EnemyProjectile>().ActivateProjectile();

        // shoot the projectile
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private int FindFireBall() {

        for(int i = 0; i < fireballs.Length; i++) {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }
}
