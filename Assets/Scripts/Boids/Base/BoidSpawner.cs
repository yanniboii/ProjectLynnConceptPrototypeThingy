using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private BoidBehaviour boidGO;
    [SerializeField] private TransformListValue currentSchool;
    [SerializeField] private TransformListValue allSchools;
    [SerializeField] private Transform position;
    [SerializeField] private float radius;
    [SerializeField] private int amount;

    void Start()
    {
        allSchools.value.Add(transform);
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomRadius = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
            Vector3 spawnLocation = randomRadius + position.position;
            boidGO.boundsCenter = transform;
            boidGO.allBoidsValue = currentSchool;
            Instantiate(boidGO.gameObject, spawnLocation, Quaternion.identity);
        }
    }
}
