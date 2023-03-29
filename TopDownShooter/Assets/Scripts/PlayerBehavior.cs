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
    private bool ReloadNeeded = false;

    Vector2 inputVector = Vector2.zero;

    [Header("Prefab References")]
    [SerializeField] GameObject targetPrefab;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] private GameObject barrel;

    [Header("Designer Variables")]
    public static int maxBullets = 10;
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float velocityDampening = 0.98f;

    [SerializeField] AudioClip fire;


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
        // TODO: draw guiding line for "easy" mode 

        // spawn bullet on mouse click
        if (Input.GetButtonDown("Fire")) {
            Fire();
        }
        // if reloading permission is granted, top off ammunition
        if (Input.GetButtonDown("Reload") && canLoad) {
            bullets = maxBullets;
            print(bullets);
        }
        DisplayAmmo();
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
    // fire if ammo allows
    void Fire() {
        print(GameManager.instance.bullets);
        if (bullets > 0) {
            AudioSource.PlayClipAtPoint(fire, transform.position);
            Instantiate(bulletPrefab, bulletSpawnPos.position, transform.rotation);
            bullets -= 1;
            print(bullets);
        }
    }
    // light up "barrel" if ammo remaining, turn off if out of ammo
    void DisplayAmmo() {
        if (bullets == 0) {
            ReloadNeeded = true;
            barrel.SetActive(false);
        }
        else {
            ReloadNeeded = false;
            barrel.SetActive(true);
        }
    }
}
