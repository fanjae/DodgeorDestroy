using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private ScoreManager scoreManager;

    private bool isGameOver = false;

    private void Start()
    {
        // 게임 오버 패널 숨김
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        // 게임 오버 상태에서 아무키나 누르면 종료 시킴
        if (isGameOver && Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    public void GameOver()
    {
        // 게임 오버 된 경우 중복 실행 방지
        if (isGameOver) return;

        isGameOver = true;
        gameOverPanel.SetActive(true);

        gameOverScoreText.text = "Your Score : " + scoreManager.Score;

        // 게임 정지
        Time.timeScale = 0f;
    }
}