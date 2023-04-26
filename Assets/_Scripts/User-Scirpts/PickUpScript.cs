using System;
using UnityEngine;

namespace _Scripts
{
    public class PickUpScript : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private string _tag;
        [SerializeField] private Transform _handPos, _normalPos;

        private Ray _ray;
        private Camera _camera;
        private GameObject _heldObject;
        private bool _isHoldingObject = false, _isObjectInHand;
        
        public bool ObjectinHands
        {
            get => _isObjectInHand;
            set => _isObjectInHand = value;
        }

        private void Awake() => _camera = GetComponent<Camera>();

        void Update()
        {
            _ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (_isHoldingObject)
            {
                _heldObject.transform.position = _handPos.position;
                
                if (Input.GetMouseButtonUp(0))
                {
                    _isObjectInHand = false;
                    _heldObject.transform.SetParent(_normalPos);
                    _heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    _heldObject.GetComponent<Collider>().enabled = true;
                    _isHoldingObject = false;
                    _heldObject = null;
                }
            }
            else
            {
                if (Physics.Raycast(_ray, out var hit, _maxDistance))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.gameObject.CompareTag(_tag))
                    {
                        var pick = hitObject.GetComponent<Rigidbody>();
                        if (Input.GetMouseButtonDown(0))
                        {
                            _isObjectInHand = true;
                            _heldObject = hitObject;
                            _heldObject.transform.SetParent(_handPos);
                            _heldObject.GetComponent<Collider>().enabled = false;
                            _heldObject.GetComponent<Rigidbody>().isKinematic = true;
                            _isHoldingObject = true;
                        }
                    }
                }
            }
        }
    }    
}