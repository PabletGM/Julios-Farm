using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterPoint : MonoBehaviour
{
    public static RoomEnterPoint instance;

    private void Awake()
    {
        instance = this;
    }
}
