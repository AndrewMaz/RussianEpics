using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace Abstracts
{
    public abstract class SpawnElement: MonoBehaviour
    {
        [SerializeField] private SpriteAtlas _atlas;
        [SerializeField] private SpriteRenderer _spriteToChange;
        [SerializeField] private Sprite[] sprites;

        private int _stage;

        protected int Stage
        {
            get => _stage;

            private set
            {
                _stage = value;
                if (sprites.Count() < value) 
                    return;

                _spriteToChange.sprite = sprites[_stage];
            }
        }

        private void Awake()
        {
            if (_atlas == null) return;
            sprites = new Sprite[_atlas.spriteCount];
            _atlas.GetSprites(sprites);
            Stage = 0;
        }

 
        public void SetStage(int stage)
        {
            Stage = stage;
        }
    }
}
