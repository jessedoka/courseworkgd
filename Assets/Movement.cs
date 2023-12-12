using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // player should be able to move forward, backward, left, right, up, and down
    // player should be able to rotate left and right

    public float speed = 30f;
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // move backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        // move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // move up
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        // move down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // rotate left
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // rotate right
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }
}