using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 boundsExtents;
    [SerializeField] private float boundsRadius;
    [SerializeField] public Transform boundsCenter;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float avoidanceRadius;
    [SerializeField] private float cohesionRadius;
    [SerializeField] private float alignmentRadius;

    [Range(0, 1)]
    [SerializeField] private float boundsStrength;
    [Range(0, 1)]
    [SerializeField] private float avoidanceStrength;
    [Range(0, 1)]
    [SerializeField] private float cohesionStrength;
    [Range(0, 1)]
    [SerializeField] private float alignmentStrength;

    [SerializeField] private Vector3 direction;

    [SerializeField] private TransformListValue allBoidsValue;

    [SerializeField] private bool debug;

    private List<Transform> avoidanceBoids;
    private List<Transform> cohesionBoids;
    private List<Transform> alignmentBoids;

    Vector3 avoidanceDirection;
    Vector3 cohesionDirection;
    Vector3 alignmentDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allBoidsValue.value.Add(transform);
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;

        AvoidanceCalculation();
        CohesionCalculation();
        AlignmentCalculation();

        direction += avoidanceDirection.normalized * avoidanceStrength;
        direction += cohesionDirection.normalized * cohesionStrength;
        direction += alignmentDirection.normalized * alignmentStrength;
        direction += ToBoundsRadius(transform.position) * boundsStrength;


        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        transform.Translate(forwardSpeed * Vector3.forward, Space.Self);
    }

    private void OnDestroy()
    {
        allBoidsValue.value.Remove(transform);
    }

    void AvoidanceCalculation()
    {
        avoidanceDirection = Vector3.zero;

        if (allBoidsValue?.value == null) { avoidanceBoids.Clear(); return; }

        avoidanceBoids = allBoidsValue.value.
            Where(x => x != transform && Vector3.Distance(x.position, transform.position) < avoidanceRadius).
            ToList();

        for (int i = 0; i < avoidanceBoids.Count; i++)
        {
            Vector3 distanceToBoid = avoidanceBoids[i].position - transform.position;

            float avoidanceStrength = 1.0f - (distanceToBoid.magnitude / avoidanceRadius);
            avoidanceDirection += -distanceToBoid * avoidanceStrength;
        }
    }

    void CohesionCalculation()
    {
        cohesionDirection = Vector3.zero;
        Vector3 middlePoint = transform.position;

        if (allBoidsValue?.value == null) { cohesionBoids.Clear(); return; }

        cohesionBoids = allBoidsValue.value.
            Where(x => x != transform && Vector3.Distance(x.position, transform.position) < cohesionRadius).
            ToList();

        for (int i = 0; i < cohesionBoids.Count; i++)
        {
            middlePoint += cohesionBoids[i].position;
        }

        cohesionDirection = transform.position - middlePoint;
    }

    void AlignmentCalculation()
    {
        alignmentDirection = Vector3.zero;
        Vector3 alignmentTotal = transform.rotation.eulerAngles;

        if (allBoidsValue?.value == null) { alignmentBoids.Clear(); return; }

        alignmentBoids = allBoidsValue.value.
            Where(x => x != transform && Vector3.Distance(x.position, transform.position) < alignmentRadius).
            ToList();

        for (int i = 0; i < alignmentBoids.Count; i++)
        {
            alignmentTotal += alignmentBoids[i].rotation.eulerAngles;
        }

        alignmentDirection = alignmentTotal;
    }

    Vector3 ToBounds(Vector3 position)
    {
        Vector3 offset = position - boundsCenter.position;
        Vector3 force = Vector3.zero;

        if (Mathf.Abs(offset.x) > boundsExtents.x)
            force.x = -Mathf.Sign(offset.x);

        if (Mathf.Abs(offset.y) > boundsExtents.y)
            force.y = -Mathf.Sign(offset.y);

        if (Mathf.Abs(offset.z) > boundsExtents.z)
            force.z = -Mathf.Sign(offset.z);

        return force.normalized;
    }

    Vector3 ToBoundsRadius(Vector3 position)
    {
        Vector3 offset = position - boundsCenter.position;
        Vector3 force = Vector3.zero;

        if (offset.magnitude > boundsRadius)
            force = -offset;

        return force.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(boundsCenter.position, boundsRadius);

        Gizmos.color = Color.plum;
        Vector3 pos = boundsCenter.position + new Vector3(boundsRadius / 2, boundsRadius / 2, boundsRadius / 2);
        Gizmos.DrawWireSphere(pos, boundsRadius);
    }
}
