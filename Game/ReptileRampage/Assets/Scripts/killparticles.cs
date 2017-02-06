using UnityEngine;

public class killparticles : MonoBehaviour {

	private ParticleSystem ps;

	void Start () {
		ps = gameObject.GetComponent<ParticleSystem>();
	}

	void Update () {
		if(!ps.IsAlive()) Destroy(gameObject);
	}
}
