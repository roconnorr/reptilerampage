using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdate : MonoBehaviour {

	private Slider volumeSlider;
	public bool masterControl;

	void Start () {
		volumeSlider = gameObject.GetComponent<Slider>();
	}
	
	void Update () {
		if(masterControl && AudioListener.volume != volumeSlider.value){
			volumeSlider.value = MusicPlayer.masterVolume;
		}
		if(!masterControl && volumeSlider.value != MusicPlayer.volume){
			volumeSlider.value = MusicPlayer.volume;
		}
	}
}
