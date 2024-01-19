using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerHitEffect : MonoBehaviour
{
    
    public GameObject follow;

    private void Update()
    {
        this.transform.position = follow.transform.position;
    }
}
