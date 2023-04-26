using System;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f, _mouseSensitivity = 10.0f;
    [SerializeField] private float _maxRot;
    
    
    private float moveFB;
    private float moveLR;

    private float rotX;
    private float rotY;

    private Vector3 movement;

    void Update()
    {
        moveFB = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime;
        moveLR = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime;
        
        movement = new Vector3(moveLR, 0, moveFB);
        movement = transform.rotation * movement;
        transform.position += movement;
    }
}

