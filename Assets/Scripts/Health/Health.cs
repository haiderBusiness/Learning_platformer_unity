
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private Animator anim;

    private bool dead;

   
     [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRend;


    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;



    public bool invulnerable { get; private set; }
 



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();

        spriteRend = GetComponent<SpriteRenderer>();
    }



   public void TakeHealth (float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
        anim.SetTrigger("heal");
    }

    

    public void TakeDamage(float _damage)
    {

        if (invulnerable) return;


        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth <= 0 && !dead)
        {
            // player dies
 
            /* Die(); */


/*             // disable player movement
            if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;

            // disable enemy 
            if(GetComponentInParent<EnemyPatrol>() != null) 
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if( GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false; */

            // deactivate all attached components
            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }

            anim.SetBool("grounded", true);
            // dying animation
            anim.SetTrigger("die");
            
            dead = true;

            // play hurt sound before death sound
            if(hurtSound != null)
                SoundManager.instance.PlaySound(hurtSound);

            // play death sound
            if(deathSound != null)
                SoundManager.instance.PlaySound(deathSound);

        } else if (!dead) {
            if(hurtSound != null)
                SoundManager.instance.PlaySound(hurtSound);

            // player takes damage

            // hurt animation

            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
           /*  print("hurt"); */
            // iframes
        }
    }



// for testing
/*     private void Update() {

        if(Input.GetKeyDown(KeyCode.E)) {
            TakeDamage(1);
        }
    } */



    // igonre collision between player and enemy 


    public void Respawn() {
        dead = false;
        TakeHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        // activate all attached component classes
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
    private IEnumerator Invulnerability() {
        // igonre collation between player and enemy

        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 9, true);

        //invunerability duration

        // change the color of the player sprite to indicate invulnerability
        for (int i = 0; i < numberOfFlashes; i++) {

            spriteRend.material.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.material.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        // disable the ignore collision after the duration
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;

    }



    public void Deactivate() {
        gameObject.SetActive(false);
    }


}
