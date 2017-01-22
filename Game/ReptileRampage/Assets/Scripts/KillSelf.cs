using UnityEngine;

public class KillSelf : MonoBehaviour {

	private float Timer;

    public float lifetime;
 
    void Start(){ 
        Timer = Time.time + lifetime;
    }
 
    void Update(){ 
        if (Time.time > Timer){
            Destroy(gameObject);
        }
    }
}
