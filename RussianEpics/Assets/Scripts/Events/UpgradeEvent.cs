using Abstracts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeEvent : Event
{
    private Rune _upgradeItem;
    private PlayerStats _playerStats;
    private DialogueSystem _dialogueSystem;
    public UpgradeEvent(Rune item, PlayerStats playerStats, DialogueSystem dialogueSystem)
    {
        _upgradeItem = item;
        _playerStats = playerStats;
        _dialogueSystem = dialogueSystem;
    }
    public override void Start()
    {
        base.Start();
        _playerStats.Upgrade(_upgradeItem.GetType().ToString());
        _dialogueSystem.SetDialogue(this);
    }
    public override void Finish()
    {
        base.Finish();
    }
}
