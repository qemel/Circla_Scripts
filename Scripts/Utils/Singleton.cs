using UnityEngine;

namespace App.Scripts.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected virtual bool DestroyTargetGameObject => false;

        public static T I { get; private set; }

        public static bool IsValid() => I != null;

        /// <summary>
        /// Do not use Awake() in Singleton. You should use Init() instead.
        /// </summary>
        private void Awake()
        {
            if (I == null)
            {
                I = this as T;
                I.Init();
                return;
            }

            if (DestroyTargetGameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void Init()
        {
        }

        private void OnDestroy()
        {
            if (I == this)
            {
                I = null;
            }

            OnRelease();
        }

        protected virtual void OnRelease()
        {
        }
    }
}