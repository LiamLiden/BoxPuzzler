using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTile : MonoBehaviour
{
    public static PlayerController pc;

    public abstract void Action();

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }
}
