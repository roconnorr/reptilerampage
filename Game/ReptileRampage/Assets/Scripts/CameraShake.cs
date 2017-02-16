using UnityEngine;

public class CameraShake : MonoBehaviour {
	private float shakeAmt = 0;

	private Camera mainCamera;

	public AudioClip stomp;

	void Start(){
		mainCamera = Camera.main;
	}
	public void StartShaking(float shakeAmt) {
		if(GetComponent<Trike>() && stomp != null) {
			PlayHitSound(stomp, this.transform.position);
		}
		if(GetComponent<TRex>() && stomp != null) {
			PlayHitSound(stomp, this.transform.position);
		}
		this.shakeAmt = shakeAmt;
		InvokeRepeating("ShakeCamera", 0, .02f);
		Invoke("StopShaking", 0.16f);
	}

	void ShakeCamera() {
		if(shakeAmt>0) {
			float quakeAmtX = Random.value*shakeAmt*2 - shakeAmt;
			float quakeAmtY = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			pp.x += quakeAmtX;
			pp.y += quakeAmtY;
			mainCamera.transform.position = pp;
		}
	}

	void StopShaking() {
		CancelInvoke("ShakeCamera");
		if (mainCamera.GetComponent<CameraFollow> () == null) {
			mainCamera.transform.position = new Vector3 (0, 0, -10);
		} else if(!WayPoints.heliMoving){
			mainCamera.transform.position = CameraFollow.cameraPosition;
		}
	}
	public static void PlayHitSound(AudioClip clip, Vector3 pos){
		GameObject temp = new GameObject("TempAudio");
		temp.transform.position = pos;
		AudioSource tempSource = temp.AddComponent<AudioSource>();
		tempSource.clip = clip;
		tempSource.volume = 0.15f;
		if(!tempSource.isPlaying){
			tempSource.Play();
		}
		Destroy(temp, clip.length);
	}
}