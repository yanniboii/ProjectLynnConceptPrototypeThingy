using System;
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
        scareActionReference.action.performed += Scare();
        lureActionReference.action.performed += Lure();
    }

    private Action<InputAction.CallbackContext> Scare()
    {
        for (int i = 0; i < allSchools.value.Count; i++)
        {
            Vector3 pos = allSchools.value[i].position;

            if (Mathf.Abs(Vector3.Distance(pos, transform.position)) < scareRadius)
            {

            }
        }

        return null;
    }

    private Action<InputAction.CallbackContext> Lure()
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

        return null;
    }

    private void OnDisable()
    {
        scareActionReference.action.started -= Scare();
        lureActionReference.action.started -= Lure();
    }
}
