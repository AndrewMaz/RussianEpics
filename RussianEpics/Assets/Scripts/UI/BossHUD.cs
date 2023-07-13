using Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _bossName;
    [SerializeField] TextMeshProUGUI _percentageText;

    private float currenntHealth, maxHealth;
    private void Awake()
    {
        _slider.gameObject.SetActive(false);
    }
    private BossCharacteristicsService _bossCharacteristics;
    public void Initialize(BossCharacteristicsService bossCharacteristics)
    {
        _bossCharacteristics = bossCharacteristics;
    }
    private void OnEnable()
    {
        _bossCharacteristics.IsBossDamaged += Damaged;
        _bossCharacteristics.IsVisible += ActivateUI;
        _bossCharacteristics.IsDead += DeactivateUI;
    }
    private void OnDisable()
    {
        _bossCharacteristics.IsBossDamaged -= Damaged;
        _bossCharacteristics.IsVisible -= ActivateUI;
        _bossCharacteristics.IsDead -= DeactivateUI;
    }
    private void Damaged(int health)
    {
        currenntHealth = health;
        _slider.value = currenntHealth / maxHealth;
        _percentageText.text = currenntHealth.ToString();
    }
    public void ActivateUI(Enemy boss)
    {
        _slider.gameObject.SetActive(true);
        currenntHealth = boss.Health;
        maxHealth = boss.Health;
        _bossName.text = boss.Name;
        _percentageText.text = currenntHealth.ToString();
    }
    public void DeactivateUI()
    {
        _slider.value = 1f;
        _slider.gameObject.SetActive(false);
    }
}
