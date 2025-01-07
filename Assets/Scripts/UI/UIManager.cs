using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen; 

    [Header("Sounds")]
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject audioScreen;

     [SerializeField] private GameObject mainMenuScreen;

    
    private void Awake() {
        if(mainMenuScreen != null) {
           mainMenuScreen.SetActive(true);
        }
        if(gameOverScreen != null) {
            gameOverScreen.SetActive(false);
        } 
        if(pauseScreen != null) {
            pauseScreen.SetActive(false);
        }

        if(audioScreen != null) {
            audioScreen.SetActive(false);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {

            // if pause screen already active unpause and viceversa
            if(pauseScreen.activeInHierarchy) 
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over
    // Activate game over screen
    public void GameOver(){

        gameOverScreen.SetActive(true);
        if(gameOverSound != null) {
            SoundManager.instance.PlaySound(gameOverSound);
        }
    }


    public void Restart() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // this will return the index of the current scence that is active
        SceneManager.LoadScene(currentLevel);
    }


    public void ReturnToMainMenu() {
        
        SceneManager.LoadScene(0); // 0 because the main menu usually is the first scene 
    }

    public void Quit() {
        
        Application.Quit(); // This will quit the game (works only in build)

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Quit playing mode in the unity editor
        #endif

    }
    #endregion


    #region Pause
    public void PauseGame(bool status) {
        //If status is true pause else unpause
        pauseScreen.SetActive(status);


        
        if(status)
            Time.timeScale = 0; // time stops (game stops)
        else 
            Time.timeScale = 1; // time goes by normaly (game resumes)


        //Time.timeScale = 2; // 2x faster
        //Time.timeScale = 1.5f; // slow motion;
    }


    
    public void fromPauseToAudio(bool status) {

        //If status is true go to audio screen and deactivate pause screen and viceversa
        pauseScreen.SetActive(!status);
        audioScreen.SetActive(status);
    }

    public void FromMainMenuToAudio(bool status) {

        //If status is true go to audio screen and deactivate pause screen and viceversa
        mainMenuScreen.SetActive(!status);
        audioScreen.SetActive(status);
    }

    #endregion


    #region AudioScreen

    public void SoundsVolume() {
        SoundManager.instance.ChangeSoundsVolume(0.2f);
    }

    public void MusicVolume() {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion


    #region Main Menu

    public void Play() {
        SceneManager.LoadScene(1); // 0 because the main menu usually is the first scene 
    }



    #endregion
}
