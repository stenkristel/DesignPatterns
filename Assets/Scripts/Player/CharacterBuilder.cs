using System.Collections.Generic;
using Input;
using Player.Class;
using Player.Movement;

namespace Player
{
    public class CharacterBuilder
    {
        private Character _character;
        
        public CharacterBuilder()
        {
            _character = new Character();
        }

        public CharacterBuilder AddCharacterStats(CharacterStats stats)
        {
            _character.Stats = stats;
            return this;
        }

        public CharacterBuilder AddClass(BaseClass characterClass)
        {
            _character.Class = characterClass;
            return this;
        }

        public CharacterBuilder AddInputCommands(InputHandler inputHandler)
        {
            _character.Input = inputHandler;
            return this;
        }

        public CharacterBuilder AddMovement(PlayerMovement movement)
        {
            _character.Movement = movement;
            return this;
        }


        public Character Build()
        {
            _character.Setup();
            return _character;
        }
    }
}
