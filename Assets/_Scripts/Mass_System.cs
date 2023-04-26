using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class Mass_System : MonoBehaviour
    {
         [SerializeField] private float _maxMass;
         [SerializeField] private UnityEvent _eventMass;
         
        
        private void OnCollisionEnter(Collision collision)
        {
           var mass = collision.gameObject.GetComponent<Rigidbody>().mass;
            if (mass>= _maxMass)
            {
                _eventMass.Invoke();
            }
        }
    }
}