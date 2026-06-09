using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image hpFillImage;
    private int maxHp;

    // 초기화
    public void Initialize(int maxhealth)
    {
        maxHp = maxhealth;

        hpFillImage.fillAmount = 1.0f;

    }
    
    // 체력 변경용
    public void SetHp(int currentHp)
    {
        hpFillImage.fillAmount = (float) currentHp / maxHp;
    }

    private void Update()
    {
        
    }

}
