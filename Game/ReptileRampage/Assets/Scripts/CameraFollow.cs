using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;

	public float followDistance;
    public static Vector3 cameraPosition;
    
    void LateUpdate () {
		if(player == null){
			return;
		}
		if(player.GetComponent<Player>().canMove && Time.timeScale != 0){
			float x = player.transform.position.x + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).x - player.transform.position.x) / followDistance);
			float y = player.transform.position.y + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).y - player.transform.position.y) / followDistance);
			transform.position = new Vector3(x, y, -10);
			cameraPosition = transform.position;
		}
    }
}
