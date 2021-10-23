using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);
        
        if(movementVector == Vector3.zero)
        {
            if(Input.GetMouseButton(1))
            {
                    Vector3 camDir = Camera.main.transform.forward;
                    camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
                    transform.forward = camDir;
            }
        }
        else if (movementVector != Vector3.zero)
        {
            if(Input.GetMouseButton(1))
            {
                Vector3 camDir = Camera.main.transform.forward;
                camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
                transform.forward = camDir;
            }
            else
            {
                RotateTowardMovementVector(movementVector);
            }
        }
        
        
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if(movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}