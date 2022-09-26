using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] GameObject _hitEffect = null;
    [SerializeField] AudioClip _hitSound = null;
    [SerializeField] [Range(1f,10f)] float _duration = 5f;

    float _lifeTime;

    private void OnEnable() {
        _lifeTime = _duration;        
    }

    private void Update() {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //TODO: Play hit sound
        //TODO: play a hit animation /effect
        //TODO: damage the object we hit
        Destroy(gameObject);
    
    }
}
