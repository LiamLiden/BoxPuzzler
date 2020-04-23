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

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (moving == false && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Move(center.transform.Find("Left").position, Vector3.left));
        }
    }

    private IEnumerator Move(Vector3 point, Vector3 axis)
    {
        moving = true;
        for (int i = 0; i < (90 / stepsForRotate); i++)
        {
            transform.RotateAround(point, axis, stepsForRotate);
            yield return new WaitForSeconds(timeBetweenSteps);
        }
        center.transform.position = transform.position;
        moving = false;
    }
}
