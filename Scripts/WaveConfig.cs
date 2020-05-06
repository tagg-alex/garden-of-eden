using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnDuration = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2.0f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        //for each transform of the children within the path prefab
        {
            waveWaypoints.Add(child);
            //add the child into our new waveWaypoints list
        } 
        return waveWaypoints;
    }
    public float GetSpawnDuration()
    {
        return spawnDuration;
    }
    public float GetRandomFactor()
    {
        return spawnRandomFactor;
    }
    public float GetSpeed()
    {
        return moveSpeed;
    }
    public int EnemyNumber()
    {
        return numberOfEnemies;
    }
}
