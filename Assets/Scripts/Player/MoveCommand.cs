using System;
using UnityEngine;

namespace Player
{
    public class MoveCommand : BaseCommand
    {
        private readonly float _direction;

        public Action<float> OnMove;

        public MoveCommand(float direction)
        {
            _direction = direction;
        }

        public override void Execute()
        {
            OnMove?.Invoke(_direction);
        }
    }
}
