using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _campos;
    [SerializeField] float distance = 3f;
    [SerializeField] LayerMask mask;
    [SerializeField] Transform arms;
    [SerializeField] Transform armsPivotpoint;
    [SerializeField] Transform orientation;

    int cooldown;
    bool holdingObject;
    bool canThrow;
    GameObject currentlyHolding;

    private void Update()
    {

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        //armsPivotpoint.LookAt(_campos.transform);

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                Actions(hitInfo.collider.gameObject);
            }
        }

        if (currentlyHolding && canThrow && Input.GetKeyDown(KeyCode.Mouse0))
        {
            GorillaThrow(currentlyHolding);
        }


        ThrowCooldown();
    }

    private void GorillaPickup(GameObject obj)
    {
        if (GetComponent<Rigidbody>()){
            Destroy(obj.GetComponent<Rigidbody>());
        }

        holdingObject = true;
        if (!obj.transform.parent == arms.transform)
        {
            currentlyHolding = obj;
            obj.transform.SetParent(arms.transform);
            obj.transform.position = arms.transform.position;
            obj.transform.rotation = orientation.transform.rotation;
        }


    }

    private void GorillaDrop(GameObject obj)
    {
        holdingObject = false;
        currentlyHolding = null;

        obj.transform.transform.parent = null;
        obj.AddComponent<Rigidbody>();
    }

    private void GorillaThrow(GameObject obj)
    {
        holdingObject = false;
        currentlyHolding = null;
        canThrow = false;

        obj.transform.transform.parent = null;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 10;
    }

    private void DestroyWall(GameObject obj)
    {
        if (!holdingObject)
            Destroy(obj);
        
    }

    private void ThrowCooldown()
    {
        if (holdingObject)
        {
            if (cooldown < 10)
            {
                cooldown = cooldown + 1;
            }
            else
            {
                cooldown = 0;
                canThrow = true;
            }
        }
    }

    private void Actions(GameObject collider)
    {
        if (Input.GetMouseButtonDown(0) && collider.tag == "Destroyable")
        {
            DestroyWall(collider);
        }

        if (Input.GetMouseButtonDown(0) && collider.tag == "HeavyObject" && !holdingObject)
        {
            if (!holdingObject)
            {
                GorillaPickup(collider.gameObject);
            }
            
        }

        if (Input.GetMouseButtonDown(1) && holdingObject)
        {
            if(currentlyHolding == collider.gameObject)
            {
                GorillaDrop(collider.gameObject);
            }

        }

    }
}
