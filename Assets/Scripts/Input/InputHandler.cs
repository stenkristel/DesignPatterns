using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Input
{
    public class InputHandler
    {
        public Dictionary<BaseCommand, KeyCode> keyBindings = new();

        /*public InputHandler(Dictionary<BaseCommand, KeyCode> keyBindings)
        {
            this.keyBindings = keyBindings;
        }*/

        private void OnUpdate()
        {
            DetectInput();
        }

        private void DetectInput()
        {
            if (!UnityEngine.Input.anyKey) return;
            for (int i = 0; i < keyBindings.Count; i++)
            {
                var keyBinding = keyBindings.ElementAt(i);
                if (UnityEngine.Input.GetKey(keyBinding.Value))
                {
                    keyBinding.Key?.Execute();
                }
                if (UnityEngine.Input.GetKeyDown(keyBinding.Value))
                {
                    keyBinding.Key?.ExecuteDown();
                }
                if (UnityEngine.Input.GetKeyUp(keyBinding.Value))
                {
                    keyBinding.Key?.ExecuteUp();
                }
            }
        }

        public void ChangeKeyBinding(BaseCommand command, KeyCode newKeyCode)
        {
            keyBindings[command] =  newKeyCode;
        }
    }
}