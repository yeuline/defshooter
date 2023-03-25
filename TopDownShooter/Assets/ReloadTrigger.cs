using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTrigger : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public int bullets;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (Input.GetButtonDown("Reload")) {
            bullets = GameManager.maxBullets;
            print("MAX");
        }
    }
}
