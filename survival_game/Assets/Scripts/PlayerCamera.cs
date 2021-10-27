using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviourPunCallbacks
{
     // camera will follow this object
    public Transform targetPlayer;
    //camera transform
    public Transform camTransform;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;
 
    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
    public float turnSpeed = 4.0f;
    public Vector3 _cameraOffset;
 
    private void Start()
    {
        //_cameraOffset = transform.position - targetPlayer.position;
    }
 
    private void LateUpdate()
    {
        if(targetPlayer.GetComponent<PlayerMovement>().allowedMoving)
        {
            if(Input.GetMouseButton(2))
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up);
                _cameraOffset = camTurnAngle * _cameraOffset;
            }

            // update position
            Vector3 targetPosition = targetPlayer.position + _cameraOffset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    
            // update rotation
            transform.LookAt(targetPlayer);
        }
        
    }
}
