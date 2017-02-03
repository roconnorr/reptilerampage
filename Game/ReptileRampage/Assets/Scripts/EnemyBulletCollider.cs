using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour {

	public void TakeDamage(int amount, Quaternion dir) {
		GetComponentInParent<Enemy> ().TakeDamage (amount, dir);
	}
}
