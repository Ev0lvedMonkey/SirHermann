using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UninteractableText : MonoBehaviour
{
    [Header("Text values")]
    [SerializeField] private List<string> textList = new();

    [Header("Links")]
    [SerializeField] private TextMeshProUGUI _textField;
    private bool _isTaskActive;

    private void Start()
    {
        DisableTextArea();
    }

    public void EnableTextArea()
    {
        gameObject.SetActive(true);
        if (_isTaskActive)
            return;
        StartCoroutine(FillOutTextArea());
    }

    public void DisableTextArea()
    {
        StopCoroutine(FillOutTextArea());
        gameObject.SetActive(false);
        _isTaskActive = false;
        _textField.text = string.Empty;
    }

    private IEnumerator FillOutTextArea()
    {
        _isTaskActive = true;
        foreach (string text in textList)
        {
            _textField.text = string.Empty; 

            for (int i = 0; i < text.Length; i++)
            {
                _textField.text += text[i]; 
                yield return new WaitForSeconds(0.05f); 
            }
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(10f);
        DisableTextArea();
    }
}
