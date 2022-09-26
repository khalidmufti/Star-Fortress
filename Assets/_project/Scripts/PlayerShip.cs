using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour {
    [SerializeField] float _speed = 0.3f;
    [SerializeField] float _rotationSpeed = 30f;
    
    Rigidbody2D _rb;
    Transform _transform;
    PlayerInputActions _inputActions;

    private float _thrustAmount = 0f;
    private float _rotationAmount = 0f;
    private float _angle = 0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _transform  = this.transform;
        _inputActions = new PlayerInputActions();
        _inputActions.Ship.Enable();
    }

    private void OnEnable() {
        _inputActions.Ship.Thrust.started += OnThrustStart;
        _inputActions.Ship.Thrust.canceled += OnThrustEnd;
        _inputActions.Ship.Move.performed += OnRotateStart;
    } 

    private void OnDisable() {
        _inputActions.Ship.Thrust.started -= OnThrustStart;
        _inputActions.Ship.Thrust.canceled -= OnThrustEnd;
        _inputActions.Ship.Move.performed -= OnRotateStart;
    }
    
    private void Start() {

    }

    private void Update() {
    }

    private void FixedUpdate() {
        ApplyRotation();
        ApplyThrust();
    }

    private void ApplyRotation() {
        if (!Mathf.Approximately (0f, _rotationAmount)) {
            _angle = _transform.localEulerAngles.z;
            _angle += _rotationAmount * + Time.fixedDeltaTime;
            _transform.localRotation = Quaternion.Euler(0f, 0f, _angle);
        }                
    }

    private void ApplyThrust() {
        if (!Mathf.Approximately (0f, _thrustAmount)) {
            _rb.AddForce (transform.up * _thrustAmount * Time.fixedDeltaTime, ForceMode2D.Force);
        }                
    }

    private void OnRotateStart (InputAction.CallbackContext context) {
        Vector2 direction = context.ReadValue<Vector2>();
        _rotationAmount = _rotationSpeed * (-direction.x);
    }

    private void OnThrustStart (InputAction.CallbackContext context) {
        _thrustAmount = _speed;
    }

    private void OnThrustEnd (InputAction.CallbackContext context) {
        _thrustAmount = 0f;
    }
}
