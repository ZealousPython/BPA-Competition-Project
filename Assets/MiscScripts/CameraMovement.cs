﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //declare variables
    public Transform player;
    private float halfHeight;
    private float halfWidth;
    public float width = 20;
    public float height = 20;
    // Start is called before the first frame update

    void Start()
    {
        //set up camera and get the half height and width
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        //move the camera based on the players position and make sure the camera does not go out of bounds
        transform.position = new Vector3(player.position.x,player.position.y, -10);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-width+halfWidth, width-halfWidth), Mathf.Clamp(transform.position.y, -height+halfHeight,height-halfHeight), -10);
    }
}
