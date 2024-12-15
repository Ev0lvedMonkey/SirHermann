using UnityEngine;

public class OpenSceneBootstrap : Bootstrap
{
    [SerializeField] private DialogueManager _dialogueManager;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SetComponents()
    {
        base.SetComponents();
        _dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    protected override void RegisterServices()
    {
        base.RegisterServices();
        ServiceLocator.Current.Register(_dialogueManager);
    }
}
