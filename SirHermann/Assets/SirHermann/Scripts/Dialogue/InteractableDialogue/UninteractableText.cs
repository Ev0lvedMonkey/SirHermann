using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UninteractableText : MonoBehaviour
{
    private bool _isTaskActive;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Image _image;

    [Header("Text Values")]
    [SerializeField] private List<string> textList = new();

    [Header("Properties")]
    [SerializeField] private UnityEvent _actions;
    [SerializeField] private bool _isRotated;

    private void OnValidate()
    {
        RotateCompoents();
        SetComponentsValues();
    }


    private void Start()
    {
        DisableTextArea();
    }

    public void EnableTextArea()
    {
        if (_isTaskActive) return;
        _image.enabled = true;
        StartCoroutine(ShowTextSequence());
    }

    public void DisableTextArea()
    {
        StopCoroutine(ShowTextSequence());
        _image.enabled = false;
        _isTaskActive = false;
        _textField.text = string.Empty;
    }

    private IEnumerator ShowTextSequence()
    {
        _isTaskActive = true;
        foreach (string text in textList)
        {
            yield return ShowTextLine(text);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(3f);
        _actions.Invoke();
        DisableTextArea();
    }

    private IEnumerator ShowTextLine(string text)
    {
        _textField.text = string.Empty;

        foreach (char character in text)
        {
            _textField.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SetComponentsValues()
    {
        if (_image == null)
            _image = GetComponent<Image>();
        if (_textField == null)
            _textField = GetComponent<TextMeshProUGUI>();
    }

    private void RotateCompoents()
    {
        Vector3 rotateScale = _isRotated ? new(-1, 1, 1) : Vector3.one;
        _image.gameObject.transform.localScale = rotateScale;
        _textField.gameObject.transform.localScale = rotateScale;
    }
}
