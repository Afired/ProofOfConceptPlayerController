using Afired.Character;
using UnityEngine;

namespace Afired.Input {
    
    [RequireComponent(typeof(IsoCharacterController))]
    public class CharacterInput : MonoBehaviour {

        private IsoCharacterController _isoCharacterController;
        private InputTable _inputTable;
        
        private void Awake() {
            _isoCharacterController = GetComponent<IsoCharacterController>();
            _inputTable = new InputTable();
        }
        
        private void Start() {
            _inputTable.CharacterControls.Blink.performed += _ => _isoCharacterController.GiveBlinkInput();
        }
        
        private void Update() {
            Vector2 axisInput = _inputTable.CharacterControls.Walk.ReadValue<Vector2>();
            _isoCharacterController.GiveAxisInput(axisInput);
        }
        
        private void OnEnable() {
            _inputTable.Enable();
        }
        
        private void OnDisable() {
            _inputTable.Disable();
        }
        
        private void OnDestroy() {
            _inputTable.Dispose();
        }
        
    }
    
}
