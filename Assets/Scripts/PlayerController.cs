using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private LayerMask groundMask;

    Vector3 forward, right;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody player;

    public GameObject crosshair;
    public float moveSpeed = 20f;
    public float rotationSpeed = 15f;
    
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        /* forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; */
        mainCamera = Camera.main;
        cameraObject = Camera.main.transform;
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Aim();
    }

    void Move()
    {
        moveDirection = cameraObject.forward * Input.GetAxis("VerticalKey");
        moveDirection = moveDirection + cameraObject.right * Input.GetAxis("HorizontalKey");
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed;

        Vector3 movementVelocity = moveDirection;
        player.velocity = movementVelocity;        
    }

    void Rotate()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * Input.GetAxis("VerticalKey");
        targetDirection = targetDirection + cameraObject.right * Input.GetAxis("HorizontalKey");
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    //Aim with the mouse
    void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            //direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }

    //Get mouse position
    (bool success, Vector3 position) GetMousePosition()
    {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
            {
                // The Raycast hit something, return with the position.
                return (success: true, position: hitInfo.point);
            }
            else
            {
                // The Raycast did not hit anything.
                return (success: false, position: Vector3.zero);
            }
    }
}
