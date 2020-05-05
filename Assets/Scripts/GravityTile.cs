using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTile : ActionTile
{
    public Vector3 newGravity;
    public Vector3 newRotation;
    public string pivotDirection;

    public override void Action()
    {
        Physics.gravity = newGravity;
        pc.GravityChange(Quaternion.Euler(newRotation), pivotDirection);
    }
}
