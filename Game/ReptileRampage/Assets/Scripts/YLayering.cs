using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YLayering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}
