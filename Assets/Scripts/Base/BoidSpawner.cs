using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private BoidBehaviour boidGO;
    [SerializeField] private Transform position;
    [SerializeField] private float radius;
    [SerializeField] private int amount;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomRadius = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Vector3 spawnLocation = randomRadius + position.position;
            Instantiate(boidGO.gameObject, spawnLocation, Quaternion.identity);
        }
    }
}
