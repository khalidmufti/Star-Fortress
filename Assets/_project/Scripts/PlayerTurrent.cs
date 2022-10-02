using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurrent : MonoBehaviour {
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject __projecttilePrefab;
    [SerializeField] float _coolDown = 0.25f;
    [SerializeField] AudioSource __audioSource = null;
    [SerializeField] AudioClip _firingSound = null;
    [SerializeField] float _shootForce = 500f;

    PlayerInputActions _inputActions;
    float _fireDelay;
    Transform _shipTransform;

    private void Awake() {
        _inputActions = new PlayerInputActions();

        _inputActions.Ship.Enable();
    }

    private void Start() {
        _shipTransform = transform;      
    }

    private void OnEnable() {
        _inputActions.Ship.Fire.performed += OnFire;
        _fireDelay = 0.1f;
    }

    private void OnDisable() {
        _inputActions.Ship.Fire.performed -= OnFire;;
    }

    private void Update() {
        _fireDelay -= Time.deltaTime;
    }

    private void OnFire(InputAction.CallbackContext obj) {
        if (_fireDelay <= 0f) {
            FireProjectile();
        }
    }


    private void FireProjectile() {
        _fireDelay = _coolDown;
        GameObject projectile = Instantiate(__projecttilePrefab, _muzzle.transform.position, _shipTransform.rotation);  
        projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * _shootForce);
        __audioSource.PlayOneShot(_firingSound);

    }
}
