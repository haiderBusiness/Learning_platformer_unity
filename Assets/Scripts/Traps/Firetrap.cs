using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    [SerializeField] private float damage; 

    [Header ("Firetrap Timers")]

    [SerializeField] private float activationDealy;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // when player enters the trigger
    private bool active; // when the firetrap is active and can hit the player

    private Health playerHealth;


    [Header("Sounds")]
    [SerializeField] private AudioClip firetrapSound;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    
    private void Update() {

        // if the player is in the trigger and the firetrap is active, deal damage to the player
        if(playerHealth != null && active) {
            playerHealth.TakeDamage(damage);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Player") {
            playerHealth = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {

            playerHealth = collision.GetComponent<Health>();

            
            if(!triggered) {
               StartCoroutine(ActivateFiretrap());
            }
            
            if(active) {
                collision.GetComponent<Health>().TakeDamage(damage);

            }
        }
    }


    private IEnumerator ActivateFiretrap() {

        triggered = true;
        spriteRend.material.color = Color.red; // turn the firetrap red when it's triggered to notify the player
        yield return new WaitForSeconds(activationDealy);
        if(firetrapSound != null) {
            SoundManager.instance.PlaySound(firetrapSound);
        }
        spriteRend.material.color = Color.white; // turn the firetrap back to white
        active = true; 
        anim.SetBool("activated", true);

        // wait until x seconds pass, deactivate trap and reset all the varivalbes and animator
        yield return new WaitForSeconds(activeTime);
        active = false; 
        triggered = false;
        anim.SetBool("activated", false);

/*         active = true;
        anim.SetBool("active", true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        anim.SetBool("active", false); */
    }


}
