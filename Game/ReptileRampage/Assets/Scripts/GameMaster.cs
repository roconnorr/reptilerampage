using UnityEngine;

public class GameMaster : MonoBehaviour {
	public static Player.WeaponType slot1type = Player.WeaponType.m1911;
    public static Player.WeaponType slot2type = Player.WeaponType.empty;
	public static Player.WeaponType levelStartSlot1Type = Player.WeaponType.m1911;
	public static Player.WeaponType levelStartSlot2Type = Player.WeaponType.empty;

	public static int slot1ammo = 100;
	public static int slot2ammo;
	public static int levelStartSlot1Ammo = 100;
	public static int levelStartSlot2Ammo;

	void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	public static Transform CreateBullet(Transform prefab, Vector3 position, float knockBackForce, float angle, int damage, float speed, float range, bool dmgPlayer, bool dmgEnemy) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<Bullet>().knockBackForce = knockBackForce;
		bullet.GetComponent<Bullet>().moveSpeed = speed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().range = range;
		bullet.GetComponent<Bullet>().dmgPlayer = dmgPlayer;
		bullet.GetComponent<Bullet>().dmgEnemy = dmgEnemy;
		return bullet;
	}

	public static Transform CreateHomingBullet(Transform prefab, Vector3 position, float angle, int damage, float speed, float range, bool dmgPlayer, bool dmgEnemy, Transform target) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<BulletHoming> ().initialAngle = angle;
		bullet.GetComponent<BulletHoming>().moveSpeed = speed;
		bullet.GetComponent<BulletHoming>().damage = damage;
		bullet.GetComponent<BulletHoming>().range = range;
		bullet.GetComponent<BulletHoming> ().target = target;
		bullet.GetComponent<BulletHoming>().dmgPlayer = dmgPlayer;
		bullet.GetComponent<BulletHoming>().dmgEnemy = dmgEnemy;
		return bullet;
	}

	public static Transform CreateGrenade(Transform prefab, Vector3 position, float angle, int damage, float speed, float range, bool dmgPlayer, bool dmgEnemy) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<Grenade>().moveSpeed = speed;
		bullet.GetComponent<Grenade>().damage = damage;
		bullet.GetComponent<Grenade>().range = range;
		bullet.GetComponent<Grenade>().dmgPlayer = dmgPlayer;
		bullet.GetComponent<Grenade>().dmgEnemy = dmgEnemy;
		return bullet;
	}

	public static GameObject CreateExplosion(GameObject animationPrefab, GameObject explosionScriptPrefab, Vector3 position, int explodeDamage, float power, float radius) {
		Instantiate (animationPrefab, position, Quaternion.Euler(0, 0, 0));
		GameObject explosion = Instantiate (explosionScriptPrefab, position, Quaternion.Euler(0, 0, 0));
		explosion.GetComponent<Explosion>().position = position;
		explosion.GetComponent<Explosion>().explodeDamage = explodeDamage;
		explosion.GetComponent<Explosion>().power = power;
		explosion.GetComponent<Explosion>().radius = radius;
		return explosion;
	}
}
