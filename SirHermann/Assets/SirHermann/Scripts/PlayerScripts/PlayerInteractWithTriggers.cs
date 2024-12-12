using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerInteractWithTriggers : MonoBehaviour
{
    public Scene _selecetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp component))
        {
            component.ShowText();
            component.HideFade();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp component))
            component.HideText();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DoorsPopUp component))
        {
            if (Input.GetKey(KeyCode.E))
                component.SceneTransition();
        }
    }
}
