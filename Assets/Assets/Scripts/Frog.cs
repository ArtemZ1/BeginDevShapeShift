using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    [Tooltip("Angle of jump in Degrees")]
    [SerializeField] private float angle;

    [SerializeField] private float jumpVelocity = 10;

    [SerializeField] private Transform raycastGroundedPosition;

    private bool isGrounded = false;

    private float holdingTime = 0f;

    [SerializeField] private AnimationCurve jumpVelocityMultiplierByTime;

    
    [SerializeField] private Transform tongueTransform;
    [SerializeField] private float tongueSpeed = 10f;
    [SerializeField] private float tongueDistance = 4f;
    [SerializeField] private Coroutine tongueCoroutine = null;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space)) {
            holdingTime += Time.deltaTime;
            // rb.velocity = (transform.forward + new Vector3(0f, angle/90f, 0f)) * jumpVelocity;
        } else {
            
            if (isGrounded && Input.GetKeyUp(KeyCode.Space)) {
                rb.velocity = (transform.forward + new Vector3(0f, angle/90f, 0f)) * jumpVelocity * jumpVelocityMultiplierByTime.Evaluate(holdingTime);
            }
            holdingTime = 0f;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, tongueDistance)) {
                print(hit.transform.name);
            } else {
                tongueCoroutine ??= StartCoroutine(TongueCoroutine(
                    endPosition: Camera.main.transform.position + Camera.main.transform.forward * tongueDistance
                ));
            }
        }
        // tongueTransform.localScale += new Vector3(0, 0, 1 * Time.deltaTime);

        transform.localRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
    }

    private void FixedUpdate() {
        if (Physics.Raycast(raycastGroundedPosition.position, Vector3.down, out RaycastHit hit, 0.1f)) {
            if (hit.transform == transform) return;
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    private IEnumerator TongueCoroutine(Vector3 endPosition) {

        // print(Mathf.Atan(3f / 4f) * Mathf.Rad2Deg);

        float distance = Vector3.Distance(endPosition, tongueTransform.position);
        tongueTransform.LookAt(endPosition);

        while (true) {



            yield return null;
        }

        yield return null;

        tongueCoroutine = null;
    }

}
