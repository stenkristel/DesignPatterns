using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Input
{
    public class InputHandler
    {
        private Dictionary<KeyCode, BaseCommand> keyBindings = new();

        public void OnUpdate()
        {
            DetectInput();
        }

        private void DetectInput()
        {
            if (!UnityEngine.Input.anyKey) return;
            for (int i = 0; i < keyBindings.Count; i++)
            {
                var keyBinding = keyBindings.ElementAt(i);
                if (UnityEngine.Input.GetKey(keyBinding.Key))
                {
                    keyBinding.Value?.Execute();
                }

                if (UnityEngine.Input.GetKeyDown(keyBinding.Key))
                {
                    keyBinding.Value?.ExecuteDown();
                }

                if (UnityEngine.Input.GetKeyUp(keyBinding.Key))
                {
                    keyBinding.Value?.ExecuteUp();
                }
            }
        }

        public void AddKeyBindings(params BaseCommand[] bindings)
        {
            foreach (var command in bindings)
            {
                AddKeyBinding(command);
            }
        }

        public void AddKeyBinding(BaseCommand command)
        {
            keyBindings[command.keyCode] = command;
        }
        
        public void AddKeyBinding(KeyCode keyCode, BaseCommand command)
        {
            keyBindings[keyCode] = command;
            command.keyCode = keyCode;
        }
    }
}