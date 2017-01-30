using UnityEngine;

public class killparticles : MonoBehaviour {

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!ps.IsAlive()) Destroy(gameObject);
	}
}
