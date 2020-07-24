using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerContoller : MonoBehaviour
{
    Rigidbody myrigidbody;
    Vector3 velocity;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void LookAt(Vector3 lookpoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookpoint.x, transform.position.y, lookpoint.z);
        transform.LookAt(heightCorrectedPoint);
    }
    void FixedUpdate()
    {
        myrigidbody.MovePosition(myrigidbody.position + velocity * Time.fixedDeltaTime);
    }

}
