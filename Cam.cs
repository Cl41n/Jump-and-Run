using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float snoothSpeed = 0.215f;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
