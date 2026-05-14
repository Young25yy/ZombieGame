using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    public float rotateSpeed;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float heightOffset;
    private Vector3 newPos;
    private Quaternion newRotation;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        newPos = target.position + target.forward * zOffset + target.right * xOffset + target.up * yOffset;
        //transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime);
        transform.position = newPos;
        newRotation = Quaternion.LookRotation(target.position + target.up * heightOffset - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        transform.rotation = newRotation;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
