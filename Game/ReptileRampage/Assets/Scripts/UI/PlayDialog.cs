using UnityEngine;

public class PlayDialog : MonoBehaviour {

	public GameObject canvas;

	private TextBoxManager textBoxManager;
	public static bool atDialog0;
	public static bool atDialog6;

	public GameObject AfterTrikeDialog;

	// Use this for initialization
	void Awake(){
		textBoxManager = canvas.GetComponent<TextBoxManager>();
	}
	void Start () {
		if(AfterTrikeDialog != null){		
			AfterTrikeDialog.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Dialog0"){
			Invoke("PlayStartDialog", 0.1f);
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog1"){
			textBoxManager.SetDialogNumber(1, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog2"){
			textBoxManager.SetDialogNumber(4, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog3"){
			textBoxManager.SetDialogNumber(3, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog4"){
			Invoke("PlayL2Dialog", 0.1f);
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog5"){
			textBoxManager.SetDialogNumber(5, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog6"){
			textBoxManager.SetDialogNumber(6, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog7"){
			textBoxManager.SetDialogNumber(7, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}else if(other.tag == "Dialog8"){
			textBoxManager.SetDialogNumber(8, 1);
			textBoxManager.dialogActive = true;
			other.gameObject.SetActive(false);
		}
	}

	void PlayStartDialog(){
		textBoxManager.SetDialogNumber(0, 3);
		textBoxManager.dialogActive = true;
		atDialog0 = true;
	}
	public void ActivateAfterTrikeDialog(){
		if(AfterTrikeDialog != null){
			AfterTrikeDialog.SetActive(true);
		}
	}

	void PlayL2Dialog(){

	}

	public void PlayTrexDialog(){
		textBoxManager.SetDialogNumber(6, 5);
		atDialog6 = true;
		textBoxManager.dialogActive = true;
	}

	public void PlayGavinStartDialog(){
		textBoxManager.SetDialogNumber(8, 2);
		textBoxManager.dialogActive = true;
	}
	public void PlayAfterGavinDialog(){
		textBoxManager.SetDialogNumber(9, 2);
		textBoxManager.dialogActive = true;
	}

	public void PlayAfterGavinChadDialog(){
		textBoxManager.SetDialogNumber(10, 0);
		textBoxManager.dialogActive = true;
	}
}
