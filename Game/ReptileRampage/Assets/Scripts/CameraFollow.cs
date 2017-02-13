using UnityEngine;
//using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;
	public GameObject cutSceneObject;
	public float followDistance;
  	public static Vector3 cameraPosition;
	private Transform trex;

	void LateUpdate () {
		if(Player.scene.name == "Level1" && !WayPoints.arrived){
			if(cutSceneObject != null){
				float x = cutSceneObject.transform.position.x;
				float y = cutSceneObject.transform.position.y;
				transform.position = new Vector3(x, y, -10);
			}
		}else if(Player.scene.name == "Level2" && WayPoints.trexSpawnAnimationPlaying){
			transform.position = new Vector3(46, -15, -10);
		}
		
		if(player.GetComponent<Player>().canMove && Time.timeScale != 0){
			float x = player.transform.position.x + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).x - player.transform.position.x) / followDistance);
			float y = player.transform.position.y + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).y - player.transform.position.y) / followDistance);
			transform.position = new Vector3(x, y, -10);
			cameraPosition = transform.position;
		}
	}
}
