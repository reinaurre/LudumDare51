using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] obstacleSpawnLocations;

    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            Vector3 location = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-1f, 0.45f), -.2f) + this.transform.position;
            SpawnRandomObstacle(location);
        }
    }

    //private void SpawnRandomObstacle(Transform location)
    private void SpawnRandomObstacle(Vector3 location)
    {
        Obstacle randomObstacle = GetRandomObstacle();

        if (randomObstacle == null) return;

        GameObject newObstacle = Instantiate(randomObstacle.prefab, this.transform);
        newObstacle.transform.position = location;

        Debug.Log($"{newObstacle.name} at {newObstacle.transform.position.x},{newObstacle.transform.position.y}");
    }

    private Obstacle GetRandomObstacle()
    {
        double r = Random.Range(0, 100);

        List<Obstacle> options = new List<Obstacle>();

        for (int i = 0; i < GameManager.instance.WeightedObstacles.Length; i++)
        {
            if (GameManager.instance.WeightedObstacles[i].Chance >= r)
            {
                options.Add(GameManager.instance.WeightedObstacles[i]);
            }
        }

        if (options.Count == 0) return null;

        return options[Random.Range(0, options.Count)];
    }
}
