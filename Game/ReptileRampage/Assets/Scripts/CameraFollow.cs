using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;
	public GameObject cutSceneObject;
	public float followDistance;
  	public static Vector3 cameraPosition;
	private bool gotPosition;
	private Transform trex;
	private float originalOrthographicSize;
	private bool stop;

	void Start(){
		originalOrthographicSize = Camera.main.orthographicSize;
	}

	void LateUpdate () {
		if(Player.scene.name == "Level1" && !WayPoints.level1IntroFinished){
			if(cutSceneObject != null){
				float x = cutSceneObject.transform.position.x;
				float y = cutSceneObject.transform.position.y;
				transform.position = new Vector3(x, y, -10);
			}
		}else if(Player.scene.name == "Level1" && WayPoints.heliMoving && !stop){
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (12, 50, -15), 4*Time.deltaTime);
				StartCoroutine(ZoomOut());
				if(transform.position == new Vector3 (12, 50, -15)){
					stop = true;
				}
		}else if(Player.scene.name == "Level2" && WayPoints.trexSpawnAnimationPlaying){
			if(WayPoints.trexCam && transform.position != new Vector3(39, -22, -10)) {
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (39, -22, -10), 6*Time.deltaTime);
			}
		}else if(player.GetComponent<Player>().canMove && Time.timeScale != 0){
			if(Camera.main.orthographicSize != originalOrthographicSize && !WayPoints.heliMoving){
				StartCoroutine(ZoomIn());
			}
			float x = player.transform.position.x + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).x - player.transform.position.x) / followDistance);
			float y = player.transform.position.y + ((Camera.main.ScreenToWorldPoint (Input.mousePosition).y - player.transform.position.y) / followDistance);
			transform.position = new Vector3(x, y, -10);
			cameraPosition = transform.position;
		}

		if (!player.GetComponent<Player> ().canMove) {
			if (!gotPosition) {
				cameraPosition = transform.position;
				gotPosition = true;
			}
		} else {
			gotPosition = false;
		}
	}

	IEnumerator ZoomOut (){
        while (Camera.main.orthographicSize < 10) {
            yield return new WaitForSeconds (0.01f);
            Camera.main.orthographicSize += 0.001f;
        }
    }

	IEnumerator ZoomIn (){
        while (Camera.main.orthographicSize > originalOrthographicSize) {
            yield return new WaitForSeconds (0.005f);
            Camera.main.orthographicSize -= 0.002f;
        }
		Camera.main.orthographicSize = originalOrthographicSize;
    }
}
