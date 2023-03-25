using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    [Header("Prefab References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject remains;

    [Header("Designer Variables")]
    [SerializeField] float bulletSpeed = 50; // bullet speed
    [SerializeField] int damage; // damage to enemy

    void Start() {
        
    }
    void Update() {
        // move bullet
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject); // destroy bullet
        if (other.collider.CompareTag("Target")) {
            Destroy(other.gameObject);
            ContactPoint2D contact = other.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(remains, position, rotation); // leave remains behind
        }
    }
}
