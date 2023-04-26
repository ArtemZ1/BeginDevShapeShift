using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{

    [SerializeField] private List<GameObject> triggerIgnore;

    public Frog frogScript;

    public GameObject touchedGameObject;
    private GameObject objectOnTongue;

    public event EventHandler touchedObject;

    bool shootingTongue = true;
    bool retractingTongue = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        if (objectOnTongue != null) {
            objectOnTongue.transform.position = transform.position + transform.forward * transform.lossyScale.z;
            objectOnTongue.transform.rotation = transform.rotation;
        }
    }
    
    public IEnumerator TongueCoroutine(Vector3 endPosition) {

        print("hi");

        Cursor.lockState = CursorLockMode.Confined;

        // print(Mathf.Atan(3f / 4f) * Mathf.Rad2Deg);

        // tongueTransform.gameObject.SetActive(true);

        // var frogTongueScript = tongueTransform.GetComponent<FrogTongue>();
        // frogTongueScript.touchedObject += TongueTouchedObject;

        float distance = Vector3.Distance(endPosition, transform.position);
        transform.LookAt(endPosition);

        float distanceProgress = 0f;

        shootingTongue = true;

        while (shootingTongue) {
            distanceProgress += frogScript.tongueSpeed * Time.deltaTime;

            if (distanceProgress >= distance) {
                distanceProgress = distance;
                shootingTongue = false;
            }

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distanceProgress * 5); 

            yield return null;
        }

        retractingTongue = true;

        while (retractingTongue) {
            distanceProgress -= frogScript.tongueSpeed * Time.deltaTime;

            if (objectOnTongue == null) {
                if (distanceProgress <= 0f) {
                    distanceProgress = 0f;
                    retractingTongue = false;
                }
            } else {
                if (distanceProgress <= .2f) {
                    distanceProgress = .2f;
                    retractingTongue = false;
                }
            }

            

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distanceProgress * 5);

            if (objectOnTongue != null) {
                // objectOnTongue.transform.rotation;
                // objectOnTongue.transform.position =  
            }

            yield return null;
        }

        // yield return null;

        // frogTongueScript.touchedObject -= TongueTouchedObject;

        if (objectOnTongue == null) {
            gameObject.SetActive(false);
        }
        

        frogScript.tongueCoroutine = null;
    }

    private void OnTriggerEnter(Collider other) {
        if (triggerIgnore.Contains(other.gameObject) || other.gameObject == objectOnTongue) return;



        touchedGameObject = other.gameObject;
        // touchedObject.Invoke(this, EventArgs.Empty);


        // FrogTongue frogTongue = (FrogTongue)sender;

        var tongeableObject = touchedGameObject.GetComponent<ITongueable>();
        if (tongeableObject != null) {
            print("JOE!!");
            objectOnTongue = touchedGameObject;
            // objectOnTongue.transform.parent = transform;

            // objectOnTongue.transform.localRotation = Quaternion.identity;
        } else {
            shootingTongue = false;
        }

    }
}
