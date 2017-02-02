﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour {
    public float speed = 0.1F;
    private float startTime;
    private float journeyLength;

	private Transform startMarker;
	public Transform target;
	private Camera camera;
    public GameObject canvas;
    
    void Start() {
		camera = GetComponent<Camera>();
        startTime = Time.time;
		startMarker = camera.transform;
        journeyLength = Vector3.Distance(startMarker.position, target.position);
    }
    
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        camera.transform.position = Vector3.Lerp(startMarker.position, target.position, fracJourney);
		camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -10);
    }

    void OnTriggerEnter2D(Collider2D other){
        canvas.GetComponent<TitleScreenManager>().EnableButtons();
    }
}