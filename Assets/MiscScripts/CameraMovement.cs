using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private float halfHeight;
    private float halfWidth;
    // Start is called before the first frame update

    void Start()
    {
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,player.position.y, -10);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-20+halfWidth, 20-halfWidth), Mathf.Clamp(transform.position.y, -20+halfHeight,20-halfHeight), -10);
    }
}
