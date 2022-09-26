using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurrent : MonoBehaviour {
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject __projecttilePrefab;

    PlayerInputActions _inputActions;

    private void Awake() {
        _inputActions.Ship.Enable();
    }

    private void OnEnable() {
        _inputActions.Ship.Fire.performed += OnFire;
    }

    private void OnDisable() {
        _inputActions.Ship.Fire.performed -= OnFire;;
    }

    private void OnFire(InputAction.CallbackContext obj) {
    }


}
