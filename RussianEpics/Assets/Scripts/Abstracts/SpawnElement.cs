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

        private int _stage;

        private Motor _motor;

        private ScoreSystem _scoreSystem;
        private SpeedControlService _speedControlService;
        private Timer _timer;
        public virtual SpawnElement Initialize(ScoreSystem scoreSystem, SpeedControlService speedControlService, Timer timer)
        {
            _scoreSystem = scoreSystem;
            _speedControlService = speedControlService;
            _motor = GetComponent<Motor>();
            ChangeSpeed();
            _timer = timer;
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
        protected float Speed
        {
            get => _motor.Speed;
        }
        protected void Awake()
        {
            if (_atlas == null) return;
            sprites = new Sprite[_atlas.spriteCount];
            _atlas.GetSprites(sprites);
            Stage = 0;
        }
        public void AddScore(float amount)
        {
            _scoreSystem.AddPoints(amount);
        }
        public void SetStage(int stage)
        {
            Stage = stage;
        }
        protected virtual void ChangeSpeed()
        {
            _motor.SetMultiply(_speedControlService.Multiply);
        }
        protected void SetSpeed(float value)
        {
            _motor.Speed = value;
        }
        protected void StartTimer()
        {
            _timer.StartTimer();
        }
        protected void SetTimer(float seconds)
        {
            _timer.StartSeconds = seconds;
        }
        public virtual void ReactToTimer()
        {

        }
        protected void OnEnable()
        {
            if (_speedControlService != null)
            {
                _speedControlService.OnSpeedChange += ChangeSpeed;
            }
            if (_timer != null)
            {
                _timer.OnFinish += ReactToTimer;
            }
        }
        protected void OnDisable()
        {
            if (_speedControlService != null)
            {
                _speedControlService.OnSpeedChange -= ChangeSpeed;
            }
            if (_timer != null)
            {
                _timer.OnFinish -= ReactToTimer;
            }
        }
    }
}
