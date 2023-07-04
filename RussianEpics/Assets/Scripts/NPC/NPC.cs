using Abstracts;
using UnityEngine;

public class NPC : SpawnElement
{
    [SerializeField] string _name;
    public string Name { get { return _name; } }
}
