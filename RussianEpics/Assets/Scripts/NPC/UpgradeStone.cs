using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStone : NPC
{
    [SerializeField] private SpriteRenderer _upgradeSprite;
    [SerializeField] private Rune[] _upgradeItems;
    [SerializeField] private Sprite[] _sprites;

    private UpgradeEvent _event;

    public override SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer, DialogueSystem dialogueSystem, PlayerStats playerStats)
    {
        _event = new(GetItem(), playerStats, dialogueSystem);

        return base.Initialize(scoreSystem, speedControlService, timer, dialogueSystem, playerStats);
    }
    private Rune GetItem()
    {
        int randNumber = Random.Range(0, _upgradeItems.Length);

        _upgradeSprite.sprite = _sprites[randNumber];

        return _upgradeItems[randNumber];
    }
    private new void OnEnable()
    {
        base.OnEnable();
        SetEventItem(_event);
    }
}
