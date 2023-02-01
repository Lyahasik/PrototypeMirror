using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class PointStorage
    {
        private static List<SpawnPoint> _spawnPoints = new ();

        public static void AddPoint(SpawnPoint point)
        {
            _spawnPoints.Add(point);
        }

        public static void RemovePoint(SpawnPoint point)
        {
            _spawnPoints.Remove(point);
        }

        public static SpawnPoint GetPoint()
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

        public static void UnlockedPoints()
        {
            foreach (SpawnPoint point in _spawnPoints)
            {
                point.IsLock = false;
            }
        }
    }
}
