using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    private Vector3 mousePos;
    public Transform target; //target
    private Vector3 objectPos;
    public float angle;
    public int bullets;
    private bool canLoad;

    Vector2 inputVector = Vector2.zero;

    [Header("Prefabs References")]
    [SerializeField] GameObject targetPrefab;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPos;

    [Header("Designer Variables")]
    public static int maxBullets = 10;
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float velocityDampening = 0.98f;

    void Start() {
        bullets = maxBullets;
    }

    void Update() {
        //Collect Input
        //Axes are defined in the Project Setting > Input Manager
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else {
            inputVector = Vector2.zero;
        }

        //rotate
        mousePos = Input.mousePosition;
        mousePos.z = 0;
        //convert space to point on screen
        objectPos = Camera.main.WorldToScreenPoint(target.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // TODO: draw guiding line  

        // spawn bullet on mouse click
        if (Input.GetButtonDown("Fire")) {
            print(GameManager.instance.bullets);
            // ammo check
            if (bullets > 0) {
                Instantiate(bulletPrefab, bulletSpawnPos.position, transform.rotation);
                bullets -= 1;
                print(bullets);
            }
        }
        if (Input.GetButtonDown("Reload") && canLoad) {
            bullets = maxBullets;
            print(bullets);
        }
    }

    private void FixedUpdate() {
        //move player with input
        rb.AddForce(moveSpeed * Time.deltaTime * inputVector);
        rb.velocity *= velocityDampening;
    }
    public void OnTriggerEnter2D(Collider2D other) {
        // on entering trigger zone, relay player can load
        if (other.CompareTag("Load")) {
            canLoad = true;
            Debug.Log("Hi");
        }
    }
    public void OnTriggerExit2D(Collider2D other) {
        // if player leaves zone, stops load privileges
        if (other.CompareTag("Load")) {
            canLoad = false;
            Debug.Log("Bye");
        }

    }
}
