using TMPro;
using UnityEngine;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private ScoreSystem _scoreSystem;
    public void Initialize(ScoreSystem scoreSystem)
    {
        _scoreSystem = scoreSystem;

        enabled = true;
    }
    private void OnEnable()
    {
        _scoreSystem.OnScoreChange += ChangeUI;
    }
    private void OnDisable()
    {
        _scoreSystem.OnScoreChange -= ChangeUI;
    }
    private void ChangeUI()
    {
        _scoreText.text = _scoreSystem.TotalScore.ToString();
    }
}
