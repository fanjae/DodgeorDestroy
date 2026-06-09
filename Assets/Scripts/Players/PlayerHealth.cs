using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image[] lifeImage;
    [SerializeField] private int maxHp = 3;
    private int currentHp;

    public bool IsDead => currentHp <= 0; 
    void Start()
    {
        currentHp = maxHp;
    }
    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHp -= damage;
        
        UpdateUI();
        if(currentHp <= 0)
        {
            gameManager.GameOver();
        }
    }

    private void UpdateUI()
    {
        for(int i = 0; i < lifeImage.Length; i++)
        {
            if (i < currentHp)
            {
                lifeImage[i].enabled = true;
            }
            else
            {
                lifeImage[i].enabled = false;
            }
        }
    }
}
