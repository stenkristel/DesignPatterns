using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private readonly Transform _transform;
        public MoveCommand UpMovement { get; private set; }
        public MoveCommand DownMovement { get; private set; }
        public float Speed { get; set; }
        
        public PlayerMovement(MoveCommand upMovement, MoveCommand downMovement, Transform transform)
        {
            UpMovement = upMovement;
            DownMovement = downMovement;
            _transform = transform;
            UpMovement.onMove += MoveVertical;
            DownMovement.onMove += MoveVertical;
        }
        
        public void OnDestroy()
        {
            UpMovement.onMove -= MoveVertical;
            DownMovement.onMove -= MoveVertical;
        }

        private void MoveVertical(float direction)
        {
            var positionChange = direction * Speed * Time.deltaTime;
            var pos = _transform.position;
            pos += new Vector3(0, positionChange, 0);
            _transform.position = pos;
        }

        
    }
}
