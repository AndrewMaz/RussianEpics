
using System;
using UnityEngine.PlayerLoop;

public class OldManQuestEvent : Event
{
    private DialogueSystem _dialogueSystem;

    public OldManQuestEvent(DialogueSystem dialogueSystem)
    {
        _dialogueSystem = dialogueSystem;
    }
    public override void Start()
    {
        base.Start();
        _dialogueSystem.SetDialogue(this);
    }
    public override void Finish()
    {
        base.Finish();
    }
}
