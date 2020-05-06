using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWaveIndex = 0;
    int enemyCount;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawningAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawningAllWaves()
    {
        for (int waveIndex = startingWaveIndex; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawningNextEnemy(currentWave));
        }
    }

    private IEnumerator SpawningNextEnemy(WaveConfig waveConfig)
    {
        for (enemyCount = 0; enemyCount <= waveConfig.EnemyNumber(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.Euler(0,0,270));
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnDuration());
        }
        
        
    }


    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(SpawningNextEnemy(curr))
    }
}
