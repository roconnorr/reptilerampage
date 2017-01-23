using UnityEngine;

public class CameraShake : MonoBehaviour 
{

	Vector3 originalCameraPosition;

	float shakeAmt = 0;

	public Camera mainCamera;

	public void StartShaking(float shakeAmt) {
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
		mainCamera.transform.position = Camera2DFollow.newPos;
	}
}