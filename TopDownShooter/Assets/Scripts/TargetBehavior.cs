using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TargetBehavior : MonoBehaviour {

    public static GameManager instance;
    public GameObject player;
    public Transform breakPrefab;
    public int currentState;

    void Start() {

    }

    void Update() {
        // erase if past defense line -> game over
        if (transform.position.y < -4.5) {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            player = GameObject.FindWithTag("Player");
            Destroy(player);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        // self destruct on collision
        Destroy(gameObject);
        // destroy obstacles on collision
        Destroy(collision2D.gameObject);
    }

}
