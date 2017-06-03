using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    // The player's position.
    public Transform player;

    // Used to adjust camera in the inspector.
    public Vector2 margin, smoothing;

    // Used to set the bounds of the camera.
    public BoxCollider2D bounds;
    private Vector3 min, max;

    public bool isFollowing { get; set; }

    public void Start()
    {
        // The min and max of the box collider used as the bounds.
        min = bounds.bounds.min;
        max = bounds.bounds.max;

        isFollowing = true;
    }

    public void Update()
    {
        // The cameras position.
        var x = transform.position.x;
        var y = transform.position.y;

        // If the camera is following the player move the camera depending on the margins specified.
        if (isFollowing)
        {
            if (Mathf.Abs (x - player.position.x) > margin.x)
            {
                // We're only concerned with moving the camera horizontally, not vertically.
                x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);
            }

        }

        // Used to correct depending on the screen size.
        var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float) Screen.width / Screen.height);

        // Clamp to the new x,y position.
        x = Mathf.Clamp (x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp (y, min.y + GetComponent<Camera>().orthographicSize, max.y - GetComponent<Camera>().orthographicSize);

        // Update the camera position.
        transform.position = new Vector3 (x, y, transform.position.z);
    }
}