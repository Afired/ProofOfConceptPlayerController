using System;
using UnityEngine;

namespace Afired.Character {
    
    [RequireComponent(typeof(Rigidbody))]
    public class IsoCharacterController : MonoBehaviour {
        
        [Header("Settings")]
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _deceleration = 1f;
        [Header("Input")]
        [SerializeField] private Vector2 _axisInput;
        private Rigidbody _rigidbody;
        private Camera _camera;
        private float _accelerationLerp;
        private Vector3 _moveDirection;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }
        
        public void GiveAxisInput(Vector2 axis) {
            _axisInput = axis.normalized;
        }
        
        private void Update() {
            if(_axisInput.magnitude > 0)
                _accelerationLerp += _acceleration * Time.deltaTime;
            else
                _accelerationLerp -= _deceleration * Time.deltaTime;
            
            _accelerationLerp = Mathf.Clamp01(_accelerationLerp);
        }
        
        private void FixedUpdate() {
            if(_axisInput.magnitude > 0)
                _moveDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(_axisInput.x, 0, _axisInput.y);
            transform.position += _moveDirection * (_movementSpeed * Time.fixedDeltaTime * _accelerationLerp);
        }
        
    }
    
}
