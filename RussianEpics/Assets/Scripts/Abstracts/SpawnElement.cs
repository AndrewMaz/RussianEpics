using System;
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
        [SerializeField] private int points = 0;

        private int _stage;

        protected Motor _motor;

        private ScoreSystem _scoreSystem;
        private SpeedControlService _speedControlService;
        public virtual SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService)
        {
            _scoreSystem = scoreSystem;
            _speedControlService = speedControlService;
            _motor = GetComponent<Motor>();
            ChangeSpeed();
            enabled = true;

            return this;
        }
        protected int Stage
        {
            get => _stage;

            private set
            {
                _stage = value;
                if (sprites.Count() < value) 
                    return;
                if (_spriteToChange == null)
                    return;
                _spriteToChange.sprite = sprites[_stage];
            }
        }
        protected void Awake()
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
        public virtual void AddScore()
        {
            _scoreSystem.AddPoints(points);
        }
        protected virtual void ChangeSpeed()
        {
            _motor.SetMultiply(_speedControlService.Multiply);
        }
        protected void OnEnable()
        {
            if (_speedControlService!= null)
            {
                _speedControlService.OnSpeedChange += ChangeSpeed;
            }
        }
        protected void OnDisable()
        {
            if (_speedControlService!= null)
            {
                _speedControlService.OnSpeedChange -= ChangeSpeed;
            }
        }
    }
}
