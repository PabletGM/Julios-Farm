using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "JuliosFarm/Scriptable Variables/Float")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public float value;

    [NonSerialized]
    public float runTimeValue;

    public void OnAfterDeserialize()
    {
        runTimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}
