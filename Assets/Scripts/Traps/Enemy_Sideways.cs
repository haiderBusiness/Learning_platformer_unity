using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damge;
    
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;

    [Header("Sounds")]
    [SerializeField] private AudioClip activeSound;
    [SerializeField] private AudioClip hitPlayerSound;

    private bool activeSoundIsOn;


    private bool movingLeft; 

    private float leftEdge; 
    private float rightEdge;




    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }



    private void Update()
    {
        //TODO
/*         if(activeSound != null && SoundManager.instance != null && !activeSoundIsOn) {
            SoundManager.instance.PlaySound(activeSound, 0, true);
            activeSoundIsOn = true;
        } */
 
        if (movingLeft) 
        {
            if(transform.position.x > leftEdge) {
               transform.position = 
               new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = false;
            }
        } 
        else 
        {
            if(transform.position.x < rightEdge) {
               transform.position = 
               new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(hitPlayerSound != null) {
                SoundManager.instance.PlaySound(hitPlayerSound);
            }
            collision.GetComponent<Health>().TakeDamage(damge);
        }
    }

}
