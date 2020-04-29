using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTile : ActionTile
{
    public Vector3 newGravity;

    public override void Action()
    {
        Debug.Log(pc.center.transform.Find("Left").transform.position);
        Physics.gravity = newGravity;
        if (newGravity.z > 0)
        {
            pc.GravityChange(Quaternion.Euler(-90, 0, 0), "Right");
        }
        Debug.Log(pc.center.transform.Find("Left").transform.position);
    }
}
