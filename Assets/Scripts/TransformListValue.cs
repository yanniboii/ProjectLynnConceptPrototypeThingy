using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformListValue", menuName = "Scriptable Objects/TransformListValue")]
public class TransformListValue : ScriptableObject
{
    [System.NonSerialized]
    public List<Transform> value = new List<Transform>();
}