using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect; 
    [SerializeField] private RectTransform[] options; 
    private int currentPosition; 

    [Header("Sounds")]

    [SerializeField] private AudioClip changeSound; // for moving arrow up/down
    [SerializeField] private AudioClip interactSound; // for selecting an option

    bool arrowPositionIsSet = false;




    private void Awake() {
        rect = GetComponent<RectTransform>();
        // set the inital arrow position to the first option position 
    }


    private void Update() {

        // set the inital arrow position to the first option position 
        if(options[0] != null && !arrowPositionIsSet) {
             rect.position = new Vector3(rect.position.x, options[0].position.y  + 3, 0);
             arrowPositionIsSet = true;
        }
        //Change position of the selection arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            ChangePosition(-1);
        } else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            ChangePosition(1);
        }

        //Interact with otions
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }


    private void ChangePosition(int _change) {

        // if options is null or empty
        if(options == null || options.Length == 0) return;

        currentPosition += _change;

        if(_change != 0)
            if(changeSound != null) {
                SoundManager.instance.PlaySound(changeSound);
            }

        if(currentPosition < 0) { // if the selection was on first option and still going up 
            currentPosition = options.Length - 1; // get the selection to the last option
        } else if (currentPosition > options.Length - 1) { // if the selection was on last option and still going down
            currentPosition = 0; // get the selection to the first option
        }


        //Assign the Y position of the current option to the arrow (basically moving it up/down)
/*         rect.position = new Vector3(rect.position.x, options[currentPosition].position.y - 1, 0); */

      /*   RectTransform optionRect = options[currentPosition].GetComponent<RectTransform>();
        float offset = optionRect.rect.width / 2; // Half the width of the current option
        float textOffset = optionRect.rect.width / 2; // Half the width of the current option */
        /* rect.position = new Vector3(options[currentPosition].position.x - offset, 
        options[currentPosition].position.y, 0); */

       /*  print("rect width / 2: " + offset + " text width: " + textOffset); */
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y + 3, 0);
    }



    private void Interact() {
        if(interactSound != null) {
            SoundManager.instance.PlaySound(interactSound);
        }

         options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

    // Wait x second before invoking the button click
    private IEnumerator InvokeButtonClickWithDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
         print("InvokeButtonClickWithDelay");
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

    // not used
    public void AdjustTheInitialArrowPosition() {
        if(options[0] != null) {
             rect.position = new Vector3(rect.position.x, options[0].position.y  + 3, 0);
        }
    }
}
