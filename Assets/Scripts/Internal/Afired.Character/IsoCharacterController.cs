using System;
using UnityEngine;

namespace Afired.Character {
    
    [RequireComponent(typeof(Rigidbody))]
    public class IsoCharacterController : MonoBehaviour {
        
        [Header("Settings")]
        [SerializeField] private float _movementSpeed = 10f;
        [Header("Input")]
        [SerializeField] private Vector2 _axisInput;
        private Rigidbody _rigidbody;
        private Camera _camera;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }
        
        public void GiveAxisInput(Vector2 axis) {
            _axisInput = axis.normalized;
        }
        
        private void FixedUpdate() {
            Vector3 moveDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(_axisInput.x, 0, _axisInput.y);
            transform.position += moveDirection * (_movementSpeed * Time.fixedDeltaTime);
        }
        
    }
    
}
