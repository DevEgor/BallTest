using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static bool MoveToTarget(this Transform transform, Vector3 moveTo, float moveSpeed)
    {
        Vector3 direction = (moveTo - transform.position).normalized;
        float distance = Vector3.Distance(moveTo, transform.position);
        var offset = direction * Time.deltaTime * moveSpeed;
        offset = Vector3.ClampMagnitude(offset, distance);

        transform.position += offset;

        return moveTo.Equals(transform.position);
    }
}
