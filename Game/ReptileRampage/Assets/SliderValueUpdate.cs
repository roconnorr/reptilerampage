using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdate : MonoBehaviour {

	private Slider volumeSlider;

	void Start () {
		volumeSlider = gameObject.GetComponent<Slider>();
	}
	
	void Update () {
		volumeSlider.value = MusicPlayer.volume;
	}
}
