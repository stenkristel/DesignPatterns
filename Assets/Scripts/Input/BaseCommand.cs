using UnityEngine;

namespace Input
{
    public abstract class BaseCommand : MonoBehaviour
    {
        public KeyCode keyCode;
        protected BaseCommand(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        }
        public virtual void Execute() {}
        public virtual void ExecuteDown() {}
        public virtual void ExecuteUp() {}
    }
}