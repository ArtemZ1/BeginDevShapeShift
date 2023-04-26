using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class ShapeShift_System : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _listShapeShift;
        [SerializeField] private List<UnityEvent> _listEvents;
        [SerializeField] private PickUpScript _pickUpScript;
        private int choose;
        
        private void Update()
        {
            if (Input.GetMouseButton(1) && !_pickUpScript.ObjectinHands)
            {
                _listShapeShift[3].SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space)) choose++;


                switch (choose)
                {
                    case 0:
                        _listShapeShift[0].SetActive(true);
                        _listShapeShift[1].SetActive(false);
                        _listShapeShift[2].SetActive(false);
                        break;
                    case 1:
                        _listShapeShift[1].SetActive(true);
                        _listShapeShift[0].SetActive(false);
                        _listShapeShift[2].SetActive(false);
                        break;
                    case 2:
                        _listShapeShift[2].SetActive(true);
                        _listShapeShift[0].SetActive(false);
                        _listShapeShift[1].SetActive(false);
                        break;
                }

                if (choose > 2) choose = 0;
            }
            else
            {
                _listShapeShift[3].SetActive(false);
                
                switch (choose)
                {
                    case 0:
                        _listEvents[0].Invoke();
                        break;
                    case 1:
                        _listEvents[1].Invoke();

                        break;
                    case 2:
                        _listEvents[2].Invoke();
                        break;
                }
            }
        }
    }
    
    
}