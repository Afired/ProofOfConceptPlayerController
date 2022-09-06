using UnityEngine;

namespace Afired.Character {
    
    public class IsoCharacterController : MonoBehaviour {
        
        [SerializeField] private Vector2 _axisInput;
        
        public void GiveAxisInput(Vector2 axis) {
            _axisInput = axis.normalized;
        }
        
    }
    
}
