using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagaEvent : Event
{
    private DialogueSystem _dialogueSystem;

    public YagaEvent(DialogueSystem dialogueSystem)
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
