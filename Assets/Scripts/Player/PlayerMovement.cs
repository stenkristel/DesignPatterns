using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _speed;
        private Transform _transform;
        public MoveCommand UpMovement { get; private set; }
        public MoveCommand DownMovement { get; private set; }

        public PlayerMovement(MoveCommand upMovement, MoveCommand downMovement, Transform transform, float speed)
        {
            UpMovement = upMovement;
            DownMovement = downMovement;
            _transform = transform;
            _speed = speed;
            UpMovement.onMove += MoveVertical;
            DownMovement.onMove += MoveVertical;
        }

        public void MoveVertical(float direction)
        {
            var positionChange = direction * _speed * Time.deltaTime;
            var pos = _transform.position;
            pos += new Vector3(0, positionChange, 0);
            _transform.position = pos;
        }

        public void OnDestroy()
        {
            UpMovement.onMove -= MoveVertical;
            DownMovement.onMove -= MoveVertical;
        }
    }
}
