using Input;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace BuilderPattern
{
    public class Character :  MonoBehaviour
    {
        public CharacterStats Stats;
        public BaseClass Class;
        public InputHandler Input;
        public PlayerMovement Movement;
        
        public void Setup()
        {
            Input.keyBindings.Add(new MoveCommand(1), KeyCode.W);
            Input.keyBindings.Add(new MoveCommand(-1), KeyCode.S);
        }

        public void Paste(Character character)
        {
            Stats = character.Stats;
            Class = character.Class;
        }
    }
}
