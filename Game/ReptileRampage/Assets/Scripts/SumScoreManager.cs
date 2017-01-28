using UnityEngine;

public class SumScoreManager : MonoBehaviour {

    public static SumScoreManager instance = null;

    public int initialScore = 0;
    public bool storeHighScore = true;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); 
    }

    void Start() {
        SumScore.Reset();
        if (initialScore != 0)
            SumScore.Add(initialScore); 
        if (storeHighScore) {
            if (PlayerPrefs.HasKey("sumHS")) { 
                SumScore.HighScore = PlayerPrefs.GetInt("sumHS");
            } else {
                SumScore.HighScore = 0;
            }
        }
    }

    void OnGUI() {
      GUI.color = Color.red;   
      GUI.skin.label.alignment = TextAnchor.UpperLeft;
      GUI.skin.label.fontSize = 40;
      //GUI.skin.label.fontStyle = FontStyle.Bold;
      GUI.Label(new Rect(20,10,500,100), "Score: " + SumScore.Score);
      
      GUI.color = Color.black;
      GUI.skin.label.alignment = TextAnchor.UpperRight;
      GUI.skin.label.fontSize = 30;
      //GUI.skin.label.fontStyle = FontStyle.Bold;
      GUI.Label(new Rect(10,60,300,60), "Highest Score: " + SumScore.HighScore);
   }
}
