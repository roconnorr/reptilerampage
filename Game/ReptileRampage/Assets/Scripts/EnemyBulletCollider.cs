using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour {

	public void TakeDamage(int amount, Quaternion dir, float force) {
		GetComponentInParent<Enemy> ().TakeDamage (amount, dir, force);
	}
}
