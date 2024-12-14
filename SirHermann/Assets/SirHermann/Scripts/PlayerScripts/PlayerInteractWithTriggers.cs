using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerInteractWithTriggers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp doorsPopUp))
        {
            doorsPopUp.ShowText();
            doorsPopUp.HideFade();
        }
        if (collision.TryGetComponent(out NPCDialogueCueTrigger dialogueTrigger))
        {
            dialogueTrigger.ShowCue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp doorsPopUp))
            doorsPopUp.HideText();
        if (collision.TryGetComponent(out NPCDialogueCueTrigger dialogueTrigger))
        {
            dialogueTrigger.HideCue();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp doorsPopUp))
        {
            if (Input.GetKey(KeyCode.E))
                doorsPopUp.SceneTransition();
        }
        if (collision.TryGetComponent(out NPCDialogueCueTrigger dialogueTrigger))
        {
            if (Input.GetKeyDown(KeyCode.E))
                dialogueTrigger.EnterDialogue();
        }

    }
}
