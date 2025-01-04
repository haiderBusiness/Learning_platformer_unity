using UnityEngine;

public class HealthCollectable : MonoBehaviour
{

    [SerializeField] private float healthValue; 

    [Header("Sounds")]
    [SerializeField] private AudioClip collectSound;
    
    private bool collected;
    private bool hit; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.tag == "Player")
        {
            
            if(collectSound != null)
                SoundManager.instance.PlaySound(collectSound);

            collected = true;
            // add health to player
            collision.GetComponent<Health>().TakeHealth(healthValue);
            // destroy the health collectable
            Destroy(gameObject);
        }
    }
}
