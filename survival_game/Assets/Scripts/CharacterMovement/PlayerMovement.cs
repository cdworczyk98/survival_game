using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private InputHandler _input;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] private float MovementSpeed = 5;
    [SerializeField] private float RotationSpeed = 3;

    [SerializeField] private Camera cameraToSpawn;
    private Camera spawnedCamera;
    
    Animator _animator;

    public bool allowedMoving = false;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _input = GetComponent<InputHandler>();
    }

    private void Start() {
        if(photonView.IsMine)
        {
            spawnedCamera = Instantiate(cameraToSpawn, new Vector3(1, 5, -3), Quaternion.identity);
            spawnedCamera.GetComponent<PlayerCamera>().targetPlayer = this.transform;
            spawnedCamera.GetComponent<PlayerCamera>()._cameraOffset = new Vector3(0, 10 , -8);
        }
    }

    void Update()
    {
        
        if(photonView.IsMine)
        {
            allowedMoving = true;
            // Moving
            var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
            var movementVector = MoveTowardTarget(targetVector);

            if(movementVector == Vector3.zero && Input.GetMouseButton(1))
            {
                RotateTowardsTheMouse();
            }
            else if (movementVector != Vector3.zero)
            {
                if(Input.GetMouseButton(1))
                {
                    RotateTowardsTheMouse();
                }
                else
                {
                    RotateTowardMovementVector(movementVector);
                }
            }

                // Animating
            float velocityZ = Vector3.Dot(targetVector.normalized, transform.forward);
            float velocityX = Vector3.Dot(targetVector.normalized, transform.right);
            
            _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.main.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
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

    private void RotateTowardsTheMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var _direction = hitInfo.point - transform.position;
            _direction.y = 0f;
            _direction.Normalize();
            transform.forward = _direction;
        }
    }
}