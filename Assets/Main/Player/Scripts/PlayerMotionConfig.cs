using UnityEngine;

namespace Main.Player.Scripts
{
   [CreateAssetMenu(fileName = "PlayerMotionConfig", menuName = "Configs/Player/PlayerMotionConfig", order = 1)]
   public class PlayerMotionConfig : ScriptableObject
   {
      public float DefaultSpeed;
      public float CrouchSpeed;
      public float DefaultViewHeight;
      public float CrouchViewHeight;
      public int ViewFov;
   }
}
