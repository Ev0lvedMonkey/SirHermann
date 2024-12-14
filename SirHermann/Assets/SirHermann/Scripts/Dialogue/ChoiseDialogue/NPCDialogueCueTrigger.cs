using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class NPCDialogueCueTrigger : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Image _cueImage;
    [SerializeField] private BoxCollider2D _collider;

    [Header("Ink Data")]
    [SerializeField] private TextAsset _inkText;

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }
    }

    private void Start()
    {
        HideCue();
    }


    public void EnterDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(_inkText);

    } 
    
    public void ShowCue()
    {
        _cueImage.gameObject.SetActive(true);

    }

    public void HideCue()
    {
        _cueImage.gameObject.SetActive(false);

    }
}
