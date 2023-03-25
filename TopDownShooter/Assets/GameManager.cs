using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [Header("Prefab References")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject targetPrefab;

    [Header("Designer Variables")]
    public int approachSpeed = 50;
    public static int maxBullets = 10;
    public int bullets;

    public GameObject player { get; private set; }
    public PlayerBehavior playerScript { get; private set; }
 
    private void Awake() {
        // Singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        //spawn in player
        instance.SpawnPlayer();
        bullets = maxBullets;
        // autospawn target
        InvokeRepeating("SpawnTarget", 3, 2.0f); // wait 3 seconds, repeat every 2s 
    }

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            Time.timeScale = 0;
            if (Input.GetButtonDown("Pause")) {
                Time.timeScale = 1;
            }
        }
    }

    public void SpawnPlayer() {
        if (player == null) {
            // Instantiate and store
            player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            playerScript = player.GetComponent<PlayerBehavior>();
        }
        else {
            Debug.LogWarning("No more than one player allowed");
        }
    }
    void SpawnTarget() {
        Vector2 spawnPosition;
        Vector2 spawnMoveVector;

        spawnPosition = new Vector2(Random.Range(-8.5f, 8.5f), 6);
        spawnMoveVector = Vector2.down;

        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        if (newTarget.TryGetComponent(out Rigidbody2D rb)) {
            rb.AddForce(spawnMoveVector * approachSpeed);
        }
        else {
            Debug.LogWarning("Could not find a Rigidbody2D on the spawned target!");
        }
    }

    //enum State {
    //    title,
    //    play,
    //    gameOver
    //}
    //void ChangeScene(State gameState) {
    //    // change scenes
    //    switch (gameState) {
    //        case State.title:
    //            SceneManager.LoadScene("Title", LoadSceneMode.Single);
    //            break;
    //        case State.play:
    //            SceneManager.LoadScene("Play", LoadSceneMode.Single);
    //            break;
    //        case State.gameOver:
    //            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    //            break;
    //        default:
    //            print("Start");
    //            SceneManager.LoadScene("Play", LoadSceneMode.Single);
    //            break;
    //    }
    //}
}
