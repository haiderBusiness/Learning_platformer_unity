using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; 
    [SerializeField] private int damage; 
    [SerializeField] private float range; // Range: the distance where the enemy can see the player and attack 

    [SerializeField] private AudioClip attackSound;


    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;


    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity; 

    private Animator anim; 
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private Rigidbody2D rb;


    


    private void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

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


    private void Update() {
        cooldownTimer += Time.deltaTime; 

        // Attack only when player in sight
        if(PlayerInSight()) {
                if(cooldownTimer >= attackCooldown){
                Attack(); 
            }
        }


        if(enemyPatrol != null) {
            enemyPatrol.enabled = !PlayerInSight();
        }

    }


    private void Attack() {
        cooldownTimer = 0; 

        anim.SetTrigger("meleeAttack");
        if(attackSound != null)
            SoundManager.instance.PlaySound(attackSound);
            
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
/*         Debug.Log("Attack"); */
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



        if(hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
        }




            
        return hit.collider != null;
    }



    private void OnDrawGizmos() {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private void DamagePlayer() {
        if(PlayerInSight()) {
            // Damage player
            // if player still in range. Damage him!
            playerHealth.TakeDamage(damage);
        }
    }

}
