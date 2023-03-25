//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ReloadTrigger : MonoBehaviour
//{
//    public static GameManager instance;
//    public GameObject player;
//    public int bullets;
//    public bool canLoad = false;

//    void Start()
//    {
        
//    }
    
//    public void OnTriggerEnter2D(Collider2D other) {
//        // on entering trigger zone, relay player can load
//        if (other.CompareTag("Player")) {
//            canLoad = true;
//            Debug.Log("Hi");
//            //if (Input.GetButtonDown("Reload")) {
//            //    TopUp();
//            //    print(bullets);
//            //}
//        }
//        else canLoad = false;
//    }
//    public void OnTriggerExit2D(Collider2D other) {
//        // if player leaves zone, stops load privileges
//        if (other.CompareTag("Player")) {
//            canLoad = false;
//            Debug.Log("Bye");
//        }

//    }
//    //public int TopUp() {
//    //    bullets = PlayerBehavior.maxBullets;
//    //    return bullets;
//    //}
//}
