using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Vector3[] initialPosition; 

    private void Awake() {
        // Save the initial positions of the enemies
        initialPosition = new Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i++) {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }


    public void ActivateRoom(bool _status) {
        // Activate/deactivate the enemies in the room
        for(int i = 0; i < enemies.Length; i++) {
            if(enemies[i] != null) {
                // Reset the position of the enemies
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }

                

        }

    }
/* 
    public void ActivateFollowers (bool _status) {
        for(int i = 0; i < enemies.Length; i++) {
            if(enemies[i] != null)
                enemies[i].GetComponent<Enemy>().SetFollowing(_status);
        }
    } */

/*     public void ResetRoom(bool _status) {
        // Reset the position of the enemies
        for(int i = 0; i < enemies.Length; i++) {
            if(enemies[i] != null)
                enemies[i].transform.position = initialPosition[i];
        }
    } */
    
}
