using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound that we'll play when picking up a new checkpoint
    private Transform currentCheckpoint; // We'll store our last checkpoint here;
    private Health playerHealth;

    private UIManager uiManager; 


    private void Awake() {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }





    public void RespawnPlayer() 
    {

        //Check if checkpoint available

        if(currentCheckpoint == null) {
            // Deactive player object
            playerHealth.Deactivate();
            //show game over screen
            uiManager.GameOver();

            return; 
        } else if(checkpointSound != null) {
            SoundManager.instance.PlaySound(checkpointSound);
        }
        transform.position = currentCheckpoint.position; // Move player to check position
        playerHealth.Respawn(); // Restore player health and reset animation

         // Move the camera to the checkpoint room 
         // (this works only if checkpoint is a child of the room object )
        Camera.main.GetComponent<CameraController>().MoveToNewRooom(currentCheckpoint.parent);


    }


    // Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.transform.tag == "Checkpoint") 
        {
            currentCheckpoint = collision.transform; // Store last checkpoint that we activated as the current one

            if(checkpointSound != null) {
                SoundManager.instance.PlaySound(checkpointSound);
            }

            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); // Trigger checkpoint animation
        }   




    }


}
