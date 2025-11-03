using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] Vector3 newPos = new Vector3();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (newPos == Vector3.zero)
            GetNewPos();

        //transform.position = Vector3.Slerp(transform.position, newPos, moveSpeed);


        Quaternion targetRotation = Quaternion.LookRotation(transform.position - newPos);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        transform.Translate(moveSpeed * Vector3.back, Space.Self);

        if (Mathf.Abs(Vector3.Distance(transform.position, newPos)) < 0.1f)
            newPos = new Vector3();
    }

    private void GetNewPos()
    {
        newPos = new Vector3(Random.Range(-radius, radius),
                            Random.Range(-radius, radius),
                            Random.Range(-radius, radius));

        newPos += center.position;
    }
}
