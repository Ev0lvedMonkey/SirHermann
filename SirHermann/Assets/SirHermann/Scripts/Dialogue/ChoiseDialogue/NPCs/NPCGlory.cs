using UnityEngine;

public class NPCGlory : NPCDialogueCueTrigger
{
    public override void EnterDialogue()
    {
        Debug.Log($"EnterDialogue");
        ServiceLocator.Current.Get<DialogueManager>().EnterDialogueMode(_inkText, _funcName, GivePills);
    }

    private void GivePills() => Debug.Log($"Gived pills");

}
