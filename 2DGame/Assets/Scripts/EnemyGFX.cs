using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        transform.localScale = new Vector3(aiPath.desiredVelocity.x <= 0.01f ? -1 : 1, 1f, 1f);
    }
}
