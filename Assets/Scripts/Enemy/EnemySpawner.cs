using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyprefab;
    [SerializeField] private ScoreManager scoreManager;

    [Header("패턴 설정")]
    [SerializeField] private bool useRandomPattern = true; // 랜덤 패턴 사용 여부
    [SerializeField] private PatternType patternType = PatternType.Straight; // 고정 패턴

    [Header("스폰 설정")]
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private float spawnY = 5.5f;
    [SerializeField] private float minSpawnX = -2.5f;
    [SerializeField] private float maxSpawnX = 2.5f;

    private void Start()
    {
        StartCoroutine(SpawnCo());
    }

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // 기본 패턴 설정
        PatternType selectedPattern = patternType;

        if (useRandomPattern) // 랜덤 패턴 사용시 패턴 무작위
        {
            selectedPattern = GetRandomPattern();
        }

        float spawnX;

        if (selectedPattern == PatternType.SideOnly)
        {
            spawnX = Random.value < 0.5f ? minSpawnX : maxSpawnX;
        }
        else
        {
            spawnX = Random.Range(minSpawnX, maxSpawnX);
        }

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        Enemy enemy = Instantiate(enemyprefab, spawnPosition, Quaternion.identity);
        enemy.SetPattern(selectedPattern);
        enemy.SetScoreManager(scoreManager);
    }

    private PatternType GetRandomPattern()
    {
        return (PatternType)Random.Range(0, (int)PatternType.Count);
    }
}