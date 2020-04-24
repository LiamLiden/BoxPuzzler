using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int stepsForRotate;
    public float timeBetweenSteps;
    public GameObject center;

    private Rigidbody myRB;
    private bool moving;
    private bool upright;
    private bool horizontal;

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
        if (moving == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(Move("Left", Vector3.left));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(Move("Right", Vector3.right));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(Move("Down", Vector3.back));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(Move("Up", Vector3.forward));
            }
        }
    }

    private IEnumerator Move(string direction, Vector3 axis)
    {
        moving = true;
        for (int i = 0; i < (90 / stepsForRotate); i++)
        {
            transform.RotateAround(center.transform.Find(direction).position, axis, stepsForRotate);
            yield return new WaitForSeconds(timeBetweenSteps);
        }

        // Set new pivot locations
        if (!upright)
        {
            if (!horizontal && (direction == "Up" || direction == "Down"))
            {
                center.transform.position = transform.position + new Vector3(0, -.5f, 0);
                center.transform.Find("Down").position += new Vector3(-.5f, 0, 0);
                center.transform.Find("Up").position += new Vector3(.5f, 0, 0);
                upright = true;
            }
            else if (horizontal && (direction == "Right" || direction == "Left"))
            {
                center.transform.position = transform.position + new Vector3(0, -.5f, 0);
                center.transform.Find("Left").position += new Vector3(0, 0, .5f);
                center.transform.Find("Right").position += new Vector3(0, 0, -.5f);
                upright = true;
            }
            else
            {
                center.transform.position = transform.position;
            }
        }
        else
        {
            if (direction == "Up" || direction == "Down")
            {
                center.transform.position = transform.position;
                center.transform.Find("Down").position += new Vector3(.5f, 0, 0);
                center.transform.Find("Up").position += new Vector3(-.5f, 0, 0);
                horizontal = false;
            }
            else
            {
                center.transform.position = transform.position;
                center.transform.Find("Left").position += new Vector3(0, 0, -.5f);
                center.transform.Find("Right").position += new Vector3(0, 0, .5f);
                horizontal = true;
            }
            upright = false;
        }
        moving = false;
    }
}
