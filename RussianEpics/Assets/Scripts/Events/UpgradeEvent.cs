using Abstracts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeEvent : Event
{
    private Rune _upgradeItem;
    private PlayerStats _playerStats;
    public UpgradeEvent(Rune item, PlayerStats playerStats)
    {
        _upgradeItem = item;
        _playerStats = playerStats;
    }
    public override void Start()
    {
        base.Start();
        _playerStats.Upgrade(_upgradeItem.GetType().ToString());
    }
    public override void Finish()
    {
        base.Finish();
    }
/*    private string GetUpgradeItem()
    {
        return _upgradeItem[Random.Range(0, _upgradeItem.Length - 1)].GetType().ToString();
    }*/
}
