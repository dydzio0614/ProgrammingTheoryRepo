using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    private const float WaveSpawnUpperMargin = 1f;
    private const float EnemyHorizontalSpacing = 2f;
    
    private readonly Quaternion _rotationTowardsBottom = Quaternion.Euler(0, 180, 0);
    private int _waveCounter;
    private int _enemyCounter;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        if(_enemyCounter == 0)
            SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        float spawnPositionX = UnityEngine.Random.Range(PersistentData.Instance.LeftPlayAreaBound, (PersistentData.Instance.LeftPlayAreaBound + PersistentData.Instance.RightPlayAreaBound) / 2);
        float spawnPositionZ = PersistentData.Instance.UpperPlayAreaBound - WaveSpawnUpperMargin;

        _waveCounter++;
        
        for (int i = 0; i < _waveCounter; i++)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(spawnPositionX + i * EnemyHorizontalSpacing, 0, spawnPositionZ), _rotationTowardsBottom);
            newEnemy.GetComponent<EnemyController>().OnDeath += () => _enemyCounter--;
            _enemyCounter++;
        }
    }
}
