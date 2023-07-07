using Abstracts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    Dictionary<string, int> _levels= new()
    {
        //Runes
        { "HealthRune", 0 },
        {"MaxHealthRune", 0 },
        {"TripleShotRune", 0 },
        {"ExplosionRune", 0 },
        {"SlowRune", 0 },
        //Player
        {"Damage", 0 }
    };
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            StringBuilder stringBuilder = new();
            foreach (var level in _levels.ToList())
            {
                stringBuilder.AppendLine(level.ToString());
            }

            Debug.Log("Текущие уровни прокачки: \n" + stringBuilder.ToString());
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Upgrade("TripleShotRune");
        }
    }
    public int GetLvl(string type)
    {
        return _levels[type];
    }
    public void Upgrade(string upgradeType)
    {
        if (_levels.TryGetValue(upgradeType, out int myValue))
        {
            _levels[upgradeType] = ++myValue;
        }
    }
}
