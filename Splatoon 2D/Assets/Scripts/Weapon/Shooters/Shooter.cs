using UnityEngine;

namespace Weapon.Shooters {
    [CreateAssetMenu(fileName = "New Shooter", menuName = "Shooter")]
    public class Shooter : ScriptableObject {

        public new string name;
        public Sprite sprite;
        
        public float damage;
        public float fireRate;
        public float range;
        public float bloom;
    }
}
