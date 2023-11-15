using UnityEngine;
using UnityEngine.SceneManagement;

namespace LetMeOut.Bootstrapper
{
    public static class SceneBootstrapper
    {
        private const string SCENE_NAME = "Bootstrapped Scene";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Execute()
        {
            for (int i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (scene.name == SCENE_NAME)
                {
                    return;
                }
            }

            SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}
