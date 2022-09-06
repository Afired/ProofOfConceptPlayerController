using UnityEngine;

namespace Afired.Character {
    
    [RequireComponent(typeof(Rigidbody))]
    public class IsoCharacterController : MonoBehaviour {
        
        [Header("Settings")]
        [SerializeField] private float _maxMovementSpeed = 10f;
        [SerializeField] private AnimationCurve _accelerationCurve;
        [SerializeField] [Tooltip("Time needed to accelerate from idle to max speed")] private float _accelerationTime = 0.5f;
        [SerializeField] [Tooltip("Time needed to decelerate from max speed to idle")] private float _decelerationTime = 0.5f;
        
        [Header("References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _characterSpriteRenderer;
        
        [Header("Input Readout")]
        [SerializeField] private Vector2 _axisInput;
        
        private Rigidbody _rigidbody;
        private Camera _camera;
        private float _accelerationLerp;
        private Vector3 _moveDirection;
        private Vector3 _currentAxisMovement;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }
        
        public void GiveAxisInput(Vector2 axis) {
            _axisInput = axis.normalized;
        }
        
        private void Update() {
            if(_axisInput.magnitude > 0)
                _accelerationLerp += 1 / _accelerationTime * Time.deltaTime;
            else
                _accelerationLerp -= 1 / _decelerationTime * Time.deltaTime;
            
            _accelerationLerp = Mathf.Clamp01(_accelerationLerp);

            UpdateAnimation();
        }
        
        private void UpdateAnimation() {
            _animator.SetInteger("AnimState", _currentAxisMovement.magnitude < 0.1f ? 0 : 1);
            _characterSpriteRenderer.flipX = _currentAxisMovement.x < 0;
        }
        
        private void FixedUpdate() {
            if(_axisInput.magnitude > 0)
                _moveDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(_axisInput.x, 0, _axisInput.y);
            float interpolatedMultiplier = _accelerationLerp == 0 ? 0 : _accelerationCurve.Evaluate(_accelerationLerp);
            _currentAxisMovement = _moveDirection *_maxMovementSpeed * interpolatedMultiplier;
            transform.position += _currentAxisMovement * Time.fixedDeltaTime;
        }
        
    }
    
}
