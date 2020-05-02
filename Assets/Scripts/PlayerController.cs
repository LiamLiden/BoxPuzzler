using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int stepsForRotate;
    public float timeBetweenSteps;
    public GameObject center;
    public GameManager gm;

    [HideInInspector]
    public Rigidbody myRB;
    private List<GameObject> collidingObjects = new List<GameObject>();
    private bool moving;
    public bool upright;
    public bool horizontal;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        moving = false;
        upright = false;
        horizontal = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (moving == false && collidingObjects.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(Move("Left", -center.transform.right));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(Move("Right", center.transform.right));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(Move("Down", -center.transform.forward));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(Move("Up", center.transform.forward));
            }

            // Adjust center location depending on whether player is upright or not
            if (upright)
                center.transform.position = transform.position + (center.transform.up * -.5f);
            else
                center.transform.position = transform.position;
        }
    }

    /// <summary>
    /// Causes player to rotate around a point determined by direction and around axis.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="axis"></param>
    /// <returns></returns>
    private IEnumerator Move(string direction, Vector3 axis)
    {
        moving = true;

        for (int i = 0; i < (90 / stepsForRotate); i++)
        {
            transform.RotateAround(center.transform.Find(direction).position, axis, stepsForRotate);
            yield return new WaitForSeconds(timeBetweenSteps);
        }
        gm.IncrementMoves();
        SetPivots(direction);
        moving = false;
    }

    /// <summary>
    /// Resets the position of pivot points used by Move function.
    /// </summary>
    /// <param name="direction"></param>
    private void SetPivots(string direction)
    {
        // Set new pivot locations
        if (!upright)
        {
            if (!horizontal && (direction == "Up" || direction == "Down"))
            {
                center.transform.Find("Down").position += center.transform.right * -.5f;
                center.transform.Find("Up").position += center.transform.right * .5f;
                upright = true;
            }
            else if (horizontal && (direction == "Right" || direction == "Left"))
            {
                center.transform.Find("Left").position += center.transform.forward * .5f;
                center.transform.Find("Right").position += center.transform.forward * -.5f;
                upright = true;
            }
        }
        else
        {
            if (direction == "Up" || direction == "Down")
            {
                center.transform.Find("Down").position += center.transform.right * .5f;
                center.transform.Find("Up").position += center.transform.right * -.5f;
                horizontal = false;
            }
            else
            {
                center.transform.Find("Left").position += center.transform.forward * -.5f;
                center.transform.Find("Right").position += center.transform.forward * .5f;
                horizontal = true;
            }
            upright = false;
        }
    }

    /// <summary>
    /// Performs necessary transform updates during gravity changes.
    /// </summary>
    /// <param name="newRotation"></param>
    /// <param name="direction"></param>
    public void GravityChange(Quaternion newRotation, string direction)
    {
        center.transform.rotation = newRotation;
        SetPivots(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ActionTile"))
        {
            collision.gameObject.GetComponent<ActionTile>().Action();
        }

        collidingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        collidingObjects.Remove(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Restart"))
            gm.RestartLevel();
        else if (other.CompareTag("End"))
            gm.NextLevel();
    }
}
