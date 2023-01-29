using System.Collections.Generic;
using UnityEngine;

public class PointStorage
{
    private List<SpawnPoint> _spawnPoints = new ();

    public void AddPoint(SpawnPoint point)
    {
        _spawnPoints.Add(point);
    }

    public SpawnPoint GetPoint()
    {
        while (true)
        {
            int index = Random.Range(0, _spawnPoints.Count);

            SpawnPoint point = _spawnPoints[index];
            
            if (point.IsLock)
                continue;
            
            point.IsLock = true;
            return point;
        }
    }

    public void UnlockedPoints()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.IsLock = false;
        }
    }
}
