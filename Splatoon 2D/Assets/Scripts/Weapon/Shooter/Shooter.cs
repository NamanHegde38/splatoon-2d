using UnityEngine;

namespace Weapon.Shooter {
    [CreateAssetMenu(fileName = "New shooter", menuName = "Shooter")]
    public class Shooter : ScriptableObject {

        public new string name;
        public string type;

        public float damage;
        public float fireRate;
        public float range;
    }
}
