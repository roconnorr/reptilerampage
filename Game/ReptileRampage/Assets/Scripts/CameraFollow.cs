using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;

    public static Vector3 cameraPosition;

    void Start () 
    {
    
    }
    
    void Update () 
    {   
        transform.position = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition).x+player.transform.position.x)/4, (Camera.main.ScreenToWorldPoint(Input.mousePosition).y+player.transform.position.y)/4, -10);
        cameraPosition = transform.position;
    }
}
