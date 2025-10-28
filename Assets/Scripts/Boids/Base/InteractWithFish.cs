using UnityEngine;
using UnityEngine.InputSystem;

public class InteractWithFish : MonoBehaviour
{
    [SerializeField] private InputActionReference scareActionReference;
    [SerializeField] private InputActionReference lureActionReference;
    [SerializeField] private TransformListValue allSchools;

    [SerializeField] private float scareRadius;
    [SerializeField] private float lureRadius;


    void OnEnable()
    {

    }

    public void Scare(InputAction.CallbackContext context)
    {
        for (int i = 0; i < allSchools.value.Count; i++)
        {
            Vector3 pos = allSchools.value[i].position;

            if (Mathf.Abs(Vector3.Distance(pos, transform.position)) < scareRadius)
            {
                allSchools.value[i].position = new Vector3(Random.Range(-200, 200),
                                                            Random.Range(50, 150),
                                                            Random.Range(-200, 200));
            }
        }
    }

    public void Lure(InputAction.CallbackContext context)
    {
        float closestDistance = float.PositiveInfinity;
        Debug.Log(allSchools.value.Count);

        for (int i = 0; i < allSchools.value.Count; i++)
        {
            Vector3 pos = allSchools.value[i].position;
            float distance = Mathf.Abs(Vector3.Distance(pos, transform.position));

            if (distance < closestDistance)
            {
                allSchools.value[i].position = transform.position;
            }
        }
    }

    private void OnDisable()
    {

    }
}
