using UnityEngine;
using Weapon.Shooter;

#pragma warning disable 649
namespace Weapon {
    public class Projectile : MonoBehaviour {

        private ShooterHandler _shooterHandler;
        private Rigidbody2D _rigidbody;

        [SerializeField] private float lifeTime = 10f;
        [SerializeField] private GameObject inkSplash;
        private float _randomRot;
        
        private void Start() {
            _shooterHandler = FindObjectOfType<ShooterHandler>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddRelativeForce(new Vector2(_shooterHandler.shooter.range, 0f), ForceMode2D.Impulse);
            _randomRot = Random.Range(0f, 360f);
            Invoke(nameof(DestroyProjectile), lifeTime);
            Debug.Log(_randomRot);
        }

        private void DestroyProjectile() {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            Instantiate(inkSplash, transform.position, Quaternion.Euler(new Vector3(0f, 0f, _randomRot)) );
            DestroyProjectile();
        }
    }
}
