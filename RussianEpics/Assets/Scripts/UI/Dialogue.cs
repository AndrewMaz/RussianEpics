using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _lines;
    [SerializeField] private float _textSpeed;
    [SerializeField] private Animator _firstAnimator;
    [SerializeField] private Animator _secondAnimator;

    private int index;

    private bool _isTalking = true;
    void Start()
    {
        _text.text = string.Empty;
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        _firstAnimator.SetBool("isTalking", _isTalking);

        foreach (char c in _lines[index].ToCharArray())
        {
            _text.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    void NextLine()
    {
        _secondAnimator.SetBool("isTalking", _isTalking);
        _isTalking = !_isTalking;
        _firstAnimator.SetBool("isTalking", _isTalking);

        if (index < _lines.Length - 1)
        {
            index++;
            _text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
