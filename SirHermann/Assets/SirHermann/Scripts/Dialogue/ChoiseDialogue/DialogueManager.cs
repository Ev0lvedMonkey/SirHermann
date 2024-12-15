using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IService
{
    private Story _currentStory;
    private bool _dialogueIsPlaying;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] _buttonsChoices;
    private TextMeshProUGUI[] _choicesText;

    private void Start()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);

        InitText();
    }

    private void Update()
    {
        if (!_dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ContinueStory();
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON, string nameExternalFucn = null, Action funcBody = null)
    {
        BlockPlayerMover();
        _currentStory = new Story(inkJSON.text);
        _dialogueIsPlaying = true;
        _dialoguePanel.SetActive(true);

        if (!string.IsNullOrEmpty(nameExternalFucn) && funcBody != null)
            _currentStory.BindExternalFunction(nameExternalFucn, () => funcBody?.Invoke());
        
        ContinueStory();
        StartCoroutine(SelectFirstChoice());
    }

    private void BlockPlayerMover()
    {
        ServiceLocator.Current.Get<PlayerMovement>().SetCanCrouch(false);
        ServiceLocator.Current.Get<PlayerMovement>().SetCanRun(false);
        ServiceLocator.Current.Get<PlayerMovement>().SetCanJump(false);
    }

    private void OpenPlayerMover()
    {
        ServiceLocator.Current.Get<PlayerMovement>().SetCanCrouch(true);
        ServiceLocator.Current.Get<PlayerMovement>().SetCanRun(true);
        ServiceLocator.Current.Get<PlayerMovement>().SetCanJump(true);
    }



    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueText.text = string.Empty;
        OpenPlayerMover();
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            _dialogueText.text = _currentStory.Continue();
            if (string.IsNullOrWhiteSpace(_dialogueText.text))
                ContinueStory();
            else
                DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void InitText()
    {
        _choicesText = new TextMeshProUGUI[_buttonsChoices.Length];
        int index = 0;
        foreach (GameObject choice in _buttonsChoices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        if (currentChoices.Count > _buttonsChoices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            _buttonsChoices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < _buttonsChoices.Length; i++)
        {
            _buttonsChoices[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_buttonsChoices[0].gameObject);
    }
}
