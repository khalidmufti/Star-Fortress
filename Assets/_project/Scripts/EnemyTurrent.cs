using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurrent : MonoBehaviour {
    Transform _target;
    [SerializeField] GameObject _muzzle;
    [SerializeField] [Range(0,10f)] float _rotateSpeed = 0.01f;
    [SerializeField] [Range(0,5f)] float _initialDelay = 180f;
    [SerializeField] float _shootForce = 100f;
    [SerializeField] GameObject _projecttilePrefab;

    float _fireDelay = 0f;

    private void Start() {
        _target = FindObjectOfType<PlayerShip>(true).transform;
    }

    private void Update() {
        Vector3 vector2Target = _target.position - transform.position;
        float angle = (Mathf.Atan2 (vector2Target.y, vector2Target.x)) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotateSpeed);
 
        FireProjectile();
    }

    private void FireProjectile() {
        _fireDelay += Time.deltaTime;
        if (_fireDelay > _initialDelay) {
            _fireDelay = 0;
            GameObject projectile = Instantiate(_projecttilePrefab, _muzzle.transform.position, transform.rotation);  
            projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * _shootForce);
        }
    }

}

