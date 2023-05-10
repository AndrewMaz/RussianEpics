using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces.Infrastructure
{
    public interface IPlayerInput
    {
        public event Action IsJumped;
        public event Action<Vector2, float> IsShot;
        public event Action IsStance;
    }
}
