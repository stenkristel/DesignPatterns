using Framework.Interfaces;
using Input;
using Player.Class;
using Player.Movement;
using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour, IUpdatable
    {
        public CharacterStats Stats { get; set; }
        public BaseClass Class { get; set; }
        public InputHandler Input { get; set; }
        public PlayerMovement Movement { get; set; }
        
        public void Setup()
        {
            Input.AddKeyBindings(Movement.UpMovement, Movement.DownMovement);
            Movement.Speed = Stats.Speed;
        }

        public void OnUpdate()
        {
            Input.OnUpdate();
        }

        public void Paste(Character character)
        {
            Stats = character.Stats;
            Class = character.Class;
            Input = character.Input;
            Movement = character.Movement;
        }
    }
}
