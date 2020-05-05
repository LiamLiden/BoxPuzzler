using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTile : ActionTile
{
    public Vector3 jump;

    public override void Action()
    {
        pc.myRB.velocity = jump;
    }
    

}
