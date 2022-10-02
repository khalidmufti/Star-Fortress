using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSegment : MonoBehaviour, IDamageable {

    [SerializeField] int _hitPoints = 10;

    public void TakeDamage(int damage) {
        //Tell GameManager to add points
        gameObject.SetActive(false);
    }

}
