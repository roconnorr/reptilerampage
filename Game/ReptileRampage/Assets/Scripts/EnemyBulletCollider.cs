using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour {

	public void TakeDamage(int amount, Quaternion dir, float force, Transform source, bool isExplosion, bool isFire) {
		GetComponentInParent<Enemy> ().TakeDamage (amount, dir, force, source, isExplosion, isFire);
	}
}
