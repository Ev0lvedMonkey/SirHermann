using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class NPCDialogueCueTrigger : MonoBehaviour
{
    [Header("Componets")]
    [SerializeField] private Image _cueImage;
    [SerializeField] private BoxCollider2D _collider;

    [Header("Ink Data")]
    [SerializeField] protected TextAsset _inkText;
    [SerializeField] protected string _funcName;

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }
    }

    private void Start() => HideCue();

    public void ShowCue() => _cueImage.gameObject.SetActive(true);

    public void HideCue() => _cueImage.gameObject.SetActive(false);

    public abstract void EnterDialogue();    
}
