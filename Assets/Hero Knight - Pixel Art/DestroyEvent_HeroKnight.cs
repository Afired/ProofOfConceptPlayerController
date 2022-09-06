using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEvent_HeroKnight : MonoBehaviour {
    
    [SerializeField] private GameObject _gameObjectToDestroy;
    // Destroy particles when animation has finished playing. 
    // destroyEvent() is called as an event in animations.
    public void destroyEvent()
    {
        Destroy(_gameObjectToDestroy);
    }
}
