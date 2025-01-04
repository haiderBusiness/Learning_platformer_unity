using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            // Load the next scene
            SceneManager.LoadScene(1);
        }
    }
}
