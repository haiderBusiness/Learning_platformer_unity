using UnityEngine;

public class Door : MonoBehaviour
{
   
   [SerializeField] private Transform previousRoom;
   [SerializeField] private Transform nextRoom;

   [SerializeField] private CameraController cam;

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision.tag == "Player")
      {
        if(collision.transform.position.x < transform.position.x) {
            cam.MoveToNewRooom(nextRoom);
            nextRoom.GetComponent<Room>().ActivateRoom(true);
            previousRoom.GetComponent<Room>().ActivateRoom(false);
            /* collision.transform.position = nextRoom.position; */
        } else {
            cam.MoveToNewRooom(previousRoom);

            nextRoom.GetComponent<Room>().ActivateRoom(false);
            previousRoom.GetComponent<Room>().ActivateRoom(true);
            /* collision.transform.position = previousRoom.position; */
        }

      }
   }



}
