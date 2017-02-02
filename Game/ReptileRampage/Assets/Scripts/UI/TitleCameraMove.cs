using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour {
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

	public Transform target;
	private Camera camera;
    void Start() {
		camera = GetComponent<Camera>();
        startTime = Time.time;
        journeyLength = Vector3.Distance(camera.transform.position, target.position);
    }
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        camera.transform.position = Vector3.Lerp(camera.transform.position, target.position, fracJourney);
    }
}