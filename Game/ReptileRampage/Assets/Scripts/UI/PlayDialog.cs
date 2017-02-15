using UnityEngine;

public class PlayDialog : MonoBehaviour {

	public GameObject canvas;

	private TextBoxManager textBoxManager;
	public static bool atDialog0;

	public GameObject AfterTrikeDialog;

	// Use this for initialization
	void Start () {
		textBoxManager = canvas.GetComponent<TextBoxManager>();
		if(AfterTrikeDialog != null){		
			AfterTrikeDialog.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Dialog0"){
			textBoxManager.SetDialogNumber(0, 3);
			textBoxManager.dialogActive = true;
			atDialog0 = true;
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
			textBoxManager.SetDialogNumber(4, 1);
			textBoxManager.dialogActive = true;
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

	public void ActivateAfterTrikeDialog(){
		if(AfterTrikeDialog != null){
			AfterTrikeDialog.SetActive(true);
		}
	}
}
