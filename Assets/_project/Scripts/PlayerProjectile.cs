using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] GameObject _hitEffect = null;
    [SerializeField] AudioClip _hitSound = null;
    [SerializeField] [Range(1f,10f)] float _duration = 5f;
    [SerializeField] int _damage = 1;

    float _lifeTime;
    AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        _lifeTime = _duration;
    }

    private void Update() { 
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        SoundManager.Instance.PlaySoundEffect(_hitSound);
        Instantiate(_hitEffect, transform.position, Quaternion.identity);
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
