

using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Room camera 
   [SerializeField] private float speed; 

    private float currentPosX; 

    private Vector3 velocity = Vector3.zero;


    // Follow player

    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float aheadDistance;
     private float lookAhead;


    private void Update() {


        // Room camera movement
 /*        transform.position = Vector3.SmoothDamp(
            transform.position, 
            new Vector3(currentPosX, transform.position.y, transform.position.z), 
            ref velocity, speed); */



            // Follow player

            transform.position = 
            new Vector3(player.position.x + lookAhead, 0, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

    }

    public void MoveToNewRooom(Transform _newRoom) 
    {
        currentPosX = _newRoom.position.x;
    }
}
