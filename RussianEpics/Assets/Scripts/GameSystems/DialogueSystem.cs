using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private Dialogue _dialogue;
    private SpeedControlService _speedControlService;
    private Timer _timer;
    public void Initialize(Dialogue dialogue, SpeedControlService speedControlService, Timer timer)
    {
        _dialogue = dialogue;
        _speedControlService = speedControlService;
        _timer = timer;

        enabled = true;
    }
    private void OnEnable()
    {
        _dialogue.OnFinished += ResetSpeed;
    }
    private void OnDisable()
    {
        _dialogue.OnFinished -= ResetSpeed;
    }
    public void SetDialogue(Event eventItem)
    {
        if (!File.Exists(Application.streamingAssetsPath + "/Dialogues/" + eventItem.GetType().ToString() + ".txt"))
        {
            Debug.Log("Dialogue System: 404");
            return;
        }
        string readFromFilePath = Application.streamingAssetsPath + "/Dialogues/" + eventItem.GetType().ToString() + ".txt";

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        int i;

        for (i= 0; i < fileLines.Count; i++) 
        {
            _dialogue.AddLine(fileLines[i]);
        }
        if (i == fileLines.Count)
        {
            _speedControlService.StopSpeed();
            _dialogue.StartDialogue(eventItem.GetType().ToString());
            _timer.StopTimer();
        }
    }
    private void ResetSpeed()
    {
        _timer.ContinueTimer();
        _speedControlService.ResetSpeed();
    }
}
