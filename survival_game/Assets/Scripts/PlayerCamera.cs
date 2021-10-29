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
    private float zoomIncrement;
    private float targetZoom;

    [Header("Zoom Controls")]
    public bool zoomEnabled = true;
    public float zoomMin = 20;
    public float zoomMax = 70;
    public float zoomSpeed = 1f;
 
    private void Start()
    {
        //_cameraOffset = transform.position - targetPlayer.position;
    }
 
    private void LateUpdate()
    {
        if(targetPlayer.GetComponent<PlayerMovement>().allowedMoving)
        {
            //camera orbit
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

            //Camera Zoom
            if (zoomIncrement != (zoomMax - zoomMin) / 10)
                zoomIncrement = (zoomMax - zoomMin) / 10;

            float d = Input.GetAxis("Mouse ScrollWheel");

            if (d > 0f)
            {
                if (targetZoom > zoomMin)
                    targetZoom -= zoomIncrement;
            }
            else if (d < 0f)
            {
                if (targetZoom < zoomMax)
                    targetZoom += zoomIncrement;
            }

            if(zoomEnabled)
                GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, targetZoom, zoomSpeed * Time.deltaTime);
            }
        
    }
}
