using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputHandler _input;

    public static PlayerMovement Instance { get; private set; }

    [SerializeField] float _speed = 5f;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] private float MovementSpeed = 5;
    [SerializeField] private float RotationSpeed = 3;

    [SerializeField] private Camera Camera;
    
    Animator _animator;

    void Awake()
    {
        Instance = this;
        _animator = GetComponentInChildren<Animator>();
        _input = GetComponent<InputHandler>();
    }

    void Update()
    {
        //RotateTowardsTheMouse();


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

        // Reading the Input
      //  float horizontal = Input.GetAxis("Horizontal");
      //  float vertical = Input.GetAxis("Vertical");

      //  Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Moving
      //  if (movement.magnitude > 0)
      //  {
      //      movement.Normalize();
     //       movement *= _speed * Time.deltaTime;
     //       transform.Translate(movement, Space.World);
     //   }

        // Animating
        float velocityZ = Vector3.Dot(targetVector.normalized, transform.forward);
        float velocityX = Vector3.Dot(targetVector.normalized, transform.right);
        
        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
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