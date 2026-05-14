using System;
using Input;
using UnityEngine;

namespace Player
{
    public class MoveCommand : BaseCommand
    {
        private readonly float _direction;

        public Action<float> onMove;

        public MoveCommand(float direction, KeyCode keyCode) : base(keyCode)
        {
            _direction = direction;
        }

        public override void Execute()
        {
            onMove?.Invoke(_direction);
        }
    }
}
