using Abstracts;
using UnityEngine;

public class NPC : SpawnElement
{
    [SerializeField] string _name;

    private bool? _isFailed = null;
    public string Name { get { return _name; } }
    public bool? IsFailed { get { return _isFailed; } }

    public void FailQuest()
    {
        _isFailed = true;
    }
    public void CompleteQuest()
    {
        _isFailed = false;
    }
}
