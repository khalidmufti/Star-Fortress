using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starcastle : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log($"Starcastle hit by {other.collider.name}");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"Starcastle triggered by {other.name}");
    }
}
