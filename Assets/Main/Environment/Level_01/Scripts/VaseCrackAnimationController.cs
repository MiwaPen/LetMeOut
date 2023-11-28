using System.Collections.Generic;
using UnityEngine;

namespace Main.Environment.Level_01.Scripts
{
    public class VaseCrackAnimationController : MonoBehaviour
    {
        [SerializeField] private GameObject FullVase;
        [SerializeField] private List<GameObject> BrokenParts = new List<GameObject>();
        [SerializeField] private Animation Animation;
        private AnimationEvent _event;

        private void Start()
        {
            FullVase.SetActive(true);
            foreach (var brokenPart in BrokenParts)
            {
                brokenPart.SetActive(false);
            }
        }

        public void StarCrackAnimation()
        {
            Animation.Play(Animation.clip.name);
        }

        public void CrackEvent()
        {
            FullVase.SetActive(false);
            foreach (var brokenPart in BrokenParts)
            {
                brokenPart.SetActive(true);
            }
        }
    }
}
