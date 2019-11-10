using UnityEngine;

namespace Game {
    public class SplashHandler : MonoBehaviour {

        private SpriteRenderer _renderer;
        public Sprite[] sprites;
        
        private void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            var random = Random.Range(0, sprites.Length);
            _renderer.sprite = sprites[random];
            transform.localScale = new Vector3(Random.Range(.2f, .25f), Random.Range(.2f, .25f), 1f);
        }
    }
}
