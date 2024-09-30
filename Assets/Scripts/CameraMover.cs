using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform moveHandle;
    public float moveSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveHandle.localPosition += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHandle.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveHandle.localPosition += Vector3.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveHandle.localPosition += Vector3.back * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveHandle.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveHandle.localPosition += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
}
