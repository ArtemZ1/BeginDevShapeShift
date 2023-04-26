using System;
using UnityEngine;

namespace _Scripts
{
    public class PuzzleCell1_System : MonoBehaviour
    {
        [SerializeField] private int _groundPatts;
        [SerializeField] private GameObject _objPillar;

        public void PattsLeft() => _groundPatts--;
        private void Update()
        {
            if (_groundPatts <= 0)
            {
                _objPillar.SetActive(true);
            }
        }
    }
}