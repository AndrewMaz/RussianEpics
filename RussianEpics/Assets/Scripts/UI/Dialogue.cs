using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<string> _lines;
    [SerializeField] private float _textSpeed;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _firstAnimator;
    [SerializeField] private Animator _secondAnimator;

    private int index;

    private bool _isTalking = true;

    public event Action OnFinished;
    void Start()
    {
        _text.text = string.Empty;
    }
    void Update()
    {
        if (_panel.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            if (_text.text == _lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _text.text = _lines[index];
            }
        }
    }
    public void AddLine(string line)
    {
        _lines.Add(line);
    }
    public void StartDialogue()
    {
        index = 0;
        _panel.SetActive(true);
        StartCoroutine(TypeLine());
    }
    private IEnumerator TypeLine()
    {
        //_firstAnimator.SetBool("isTalking", _isTalking);

        foreach (char c in _lines[index].ToCharArray())
        {
            _text.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    private void NextLine()
    {
        //_secondAnimator.SetBool("isTalking", _isTalking);
        _isTalking = !_isTalking;
        //_firstAnimator.SetBool("isTalking", _isTalking);

        if (index < _lines.Count - 1)
        {
            index++;
            _text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            _lines.Clear();
            _text.text = string.Empty;
            OnFinished?.Invoke();
            _panel.SetActive(false);
        }
    }
}
