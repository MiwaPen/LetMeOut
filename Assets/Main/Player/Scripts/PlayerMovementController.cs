using UnityEngine;

namespace Main.Player.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerMotionConfig _motionConfig;
        public bool Enabled { get; private set;}

        public void Setup(PlayerMotionConfig cfg)
        {
            _motionConfig = cfg;
        }
    }
}
