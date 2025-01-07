

using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Room camera 
   [SerializeField] private float speed; 

    private float currentPosX; 

    private Vector3 velocity = Vector3.zero;


    // Follow player

    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float aheadDistance;
     private float lookAhead;

     private float edgeThreshold = 1f; // Distance from the edge at which the camera should start moving

     private Camera cam;



     
    void Start()
    {
        cam = Camera.main; // Get the main camera
    }


    private void Update() {


        // Room camera movement
/*             transform.position = Vector3.SmoothDamp(transform.position, 
            new Vector3(currentPosX, transform.position.y, transform.position.z), 
            ref velocity, speed); */
       

        // always follow player if not on wall 
        //TODO:
        if(playerMovement != null && !playerMovement.onWall()) {
             FollowPlayer();
        } else {
        }

/*         // Get the camera bounds
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        Vector2 camPosition = cam.transform.position;
        float leftEdge = camPosition.x - camWidth / 2;
        float rightEdge = camPosition.x + camWidth / 2;

        // Check if the target is near the edges and then move the camera to player position once
        if (player.position.x > rightEdge - edgeThreshold)
        {
            FollowPlayer();
        }
        else if (player.position.x < leftEdge + edgeThreshold)
        {
            FollowPlayer();
        } */

        
    }


    // Follow player

    private void FollowPlayer() {
        if(player != null) {
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        } 
    }

    public void MoveToNewRooom(Transform _newRoom) 
    {
        currentPosX = _newRoom.position.x;
    }
}
