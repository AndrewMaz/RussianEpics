using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string readFromFilePath = Path.Combine(Application.streamingAssetsPath, "Dialogues", eventItem.GetType().ToString() + ".txt");
        
        var text = Utility.ReadAllTextFromFileStreamingAssets(readFromFilePath);
        if (string.IsNullOrEmpty(text)) 
        {
            return;
        }

        List<string> fileLines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

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
