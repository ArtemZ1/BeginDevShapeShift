using System;
using UnityEngine;

namespace _Scripts
{
    public class PlayerCam : MonoBehaviour
    {
           public float mouseSensitivity, yRotLimit;
           private Vector2 rotation;

           private void Awake()
           {
               Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Locked;
           }

           void Update()
        {
            rotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotLimit, yRotLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
            transform.localRotation = yQuat/* * xQuat*/; 

            //Rotate the parent y
            transform.parent.localRotation = xQuat;
        }
    }
}