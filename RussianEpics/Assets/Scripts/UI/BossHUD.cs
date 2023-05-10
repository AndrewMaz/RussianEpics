using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    [SerializeField] Slider _slider;

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
    }
    private void OnDisable()
    {
        _bossCharacteristics.IsBossDamaged -= Damaged;
        _bossCharacteristics.IsVisible -= ActivateUI;
    }
    private void Damaged(int damage)
    {
        currenntHealth -= damage;
        _slider.value = currenntHealth / maxHealth;
    }
    public void ActivateUI(BossYaga boss)
    {
        _slider.gameObject.SetActive(true);
        currenntHealth = boss.Health;
        maxHealth = boss.Health;
    }
}
