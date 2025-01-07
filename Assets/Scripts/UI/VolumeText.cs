using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{

    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; // Sound or Music

    private TextMeshProUGUI txt; 


    private void Awake() {
        txt = GetComponent<TextMeshProUGUI>();
    }


    private void Update() {
        if(textIntro != null && volumeName != null) {
            UpdateVolume();
        }
    }
 
    private void UpdateVolume() {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volumeValue.ToString(); 
    }
}
