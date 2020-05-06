using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //GameObject wayPoint = FindObjectOfType<GameObject>;

    WaveConfig waveConfig;
    List<Transform> wayPoints;
    
    int wayPointIndex = 0;

    private void Start()
    {
        wayPoints = waveConfig.GetWaypoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointIndex < wayPoints.Count - 1)
        {
            var targetPos = wayPoints[wayPointIndex + 1].transform.position;
            var movementThisFrame = waveConfig.GetSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
