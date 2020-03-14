using UnityEngine;
using Redline.UI;

namespace Redline.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance { get; private set; }
        public JoystickController _joystickController;

        private void Awake()
        {
            instance = this;
        }
    }
}
