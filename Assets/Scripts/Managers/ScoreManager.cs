using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // 점수를 표시할 UI 텍스트

    private int score = 0;
    public int Score => score;

    private void Start()
    {
        // 시작 시 초기 점수 UI 표시
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI() // 점수 갱신
    {
        scoreText.text = "Score : " + score;
    }
}