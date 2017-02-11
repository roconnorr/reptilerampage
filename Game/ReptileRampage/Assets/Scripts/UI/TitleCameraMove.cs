using UnityEngine;

public class TitleCameraMove : MonoBehaviour {
    public float speed = 0.1F;
    private float startTime;
    private float journeyLength;
    private Vector3 pos;

	private Transform startMarker;

    private bool skip = false;

    public Transform cameraTransform;
	public Transform target;
	private Camera cam;
    public GameObject canvas;
    
    void Start() {
		cam = GetComponent<Camera>();
        startTime = Time.time;
		startMarker = cam.transform;
        journeyLength = Vector3.Distance(startMarker.position, target.position);
        pos = new Vector3(-46, -39, -10);
    }
    
    void Update() {
        if(Input.GetButton("Fire")){
            skip = true;
        }
        if(skip){
            cam.transform.position = pos;
        }else{
            Lerp();
        }
        //Lerp();
    }

    void OnTriggerEnter2D(Collider2D other){
        canvas.GetComponent<TitleScreenManager>().EnableButtons();
    }

    void Lerp(){
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector3.Lerp(startMarker.position, target.position, fracJourney);
		cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
    }
}