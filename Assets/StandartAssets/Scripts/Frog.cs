using System;
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

    public GameObject objectOnTongue = null;

    [SerializeField] private AnimationCurve jumpVelocityMultiplierByTime;

    private FrogTongue tongueScript;

    
    [SerializeField] private Transform tongueTransform;
    [SerializeField] public float tongueSpeed = 10f;
    [SerializeField] public float tongueDistance = 4f;
    public Coroutine tongueCoroutine = null;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tongueScript = tongueTransform.GetComponent<FrogTongue>();
        tongueScript.frogScript = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (tongueCoroutine == null) {
            if (isGrounded && Input.GetKey(KeyCode.Space)) {
                holdingTime += Time.deltaTime;
                // rb.velocity = (transform.forward + new Vector3(0f, angle/90f, 0f)) * jumpVelocity;
            } else {
                
                if (isGrounded && Input.GetKeyUp(KeyCode.Space)) {
                    rb.velocity = (transform.forward + new Vector3(0f, angle/90f, 0f)) * jumpVelocity * jumpVelocityMultiplierByTime.Evaluate(holdingTime);
                }
                holdingTime = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, tongueDistance)) {
                if (tongueCoroutine == null) {
                    tongueTransform.gameObject.SetActive(true);
                    tongueCoroutine ??= StartCoroutine(tongueScript.TongueCoroutine(hit.point));
                }
                
            } else {
                if (tongueCoroutine == null) {
                    tongueTransform.gameObject.SetActive(true);
                    tongueCoroutine ??= StartCoroutine(tongueScript.TongueCoroutine(
                        endPosition: Camera.main.transform.position + Camera.main.transform.forward * tongueDistance
                    ));
                }
                
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

        tongueTransform.gameObject.SetActive(true);

        var frogTongueScript = tongueTransform.GetComponent<FrogTongue>();
        frogTongueScript.touchedObject += TongueTouchedObject;

        float distance = Vector3.Distance(endPosition, tongueTransform.position);
        tongueTransform.LookAt(endPosition);

        float distanceProgress = 0f;

        bool shootingTongue = true;

        while (shootingTongue) {
            distanceProgress += tongueSpeed * Time.deltaTime;

            if (distanceProgress >= distance) {
                distanceProgress = distance;
                shootingTongue = false;
            }

            tongueTransform.localScale = new Vector3(tongueTransform.localScale.x, tongueTransform.localScale.y, distanceProgress * 5); 

            yield return null;
        }

        bool retractingTongue = true;

        while (retractingTongue) {
            distanceProgress -= tongueSpeed * Time.deltaTime;

            if (distanceProgress <= 0) {
                distanceProgress = 0;
                retractingTongue = false;
            }

            tongueTransform.localScale = new Vector3(tongueTransform.localScale.x, tongueTransform.localScale.y, distanceProgress * 5);

            if (objectOnTongue != null) {
                // objectOnTongue.transform.rotation;
                // objectOnTongue.transform.position =  
            }

            yield return null;
        }

        // yield return null;

        frogTongueScript.touchedObject -= TongueTouchedObject;

        tongueTransform.gameObject.SetActive(false);

        tongueCoroutine = null;
    }

    public void TongueTouchedObject(object sender, EventArgs e) {
        FrogTongue frogTongue = (FrogTongue)sender;

        var tongeableObject = frogTongue.touchedGameObject.GetComponent<ITongueable>();
        if (tongeableObject != null) {
            print("JOE!!");
            objectOnTongue = frogTongue.touchedGameObject;
            objectOnTongue.transform.parent = tongueTransform;

            // objectOnTongue.transform.localRotation = Quaternion.identity;
        }
    }

}
