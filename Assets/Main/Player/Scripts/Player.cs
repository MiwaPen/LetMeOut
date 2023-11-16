using UnityEngine;

namespace Main.Player.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Configs")] 
        [SerializeField] private PlayerMotionConfig _motionConfig;
        [Space]
        [Header("Components")] 
        [SerializeField] private PlayerCameraController _cameraController;
        [SerializeField] private PlayerMovementController _movementController;
        [Space]
        [Header("Colliders")]
        [SerializeField] private Collider _crouchCollider;
        [SerializeField] private Collider _defaultCollider;
    }
}
