
using UnityEngine;

using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image cuurentHealthBar;


    private void Start() {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update() {
        cuurentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
