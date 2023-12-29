using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "JuliosFarm/Scriptable Variables/Int")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public int value;

    [NonSerialized]
    public int runTimeValue;

    public void OnAfterDeserialize()
    {
        runTimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}
