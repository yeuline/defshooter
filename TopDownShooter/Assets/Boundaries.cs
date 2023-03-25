using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour {
    private Vector2 screenBounds;
    private float offset;

    void Start() {
        // calculate screen height & width
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        offset = 0.5f; // half of object size
    }

    void LateUpdate() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + offset, screenBounds.x - offset);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y -1 * -1 + offset, screenBounds.y - offset);
        transform.position = viewPos;
    }
}
