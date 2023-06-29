using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<string> _lines;
    [SerializeField] private float _textSpeed;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _anotherImage;
    [SerializeField] private List<Sprite> _sprites;

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
    public void StartDialogue(string bossName)
    {
        index = 0;
        _panel.SetActive(true);
        foreach (var sprite in _sprites)
        {
            if (bossName.Contains(sprite.name))
            {
                _anotherImage.sprite = sprite;
                break;
            }
        }
        StartCoroutine(TypeLine());
    }
    private IEnumerator TypeLine()
    {
        foreach (char c in _lines[index].ToCharArray())
        {
            _text.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    private void NextLine()
    {
        SwitchActors();

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
            ResetActors();
        }
    }
    private void SwitchActors()
    {
        (_anotherImage.gameObject.transform.localScale, _playerImage.gameObject.transform.localScale) = (_playerImage.gameObject.transform.localScale, _anotherImage.gameObject.transform.localScale);
    }
    private void ResetActors()
    {
        _anotherImage.gameObject.transform.localScale = Vector3.one;
        _playerImage.gameObject.transform.localScale = Vector3.one * 1.2f;
    }
}
