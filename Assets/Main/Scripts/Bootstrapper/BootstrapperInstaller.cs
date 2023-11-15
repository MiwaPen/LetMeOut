using LetMeOut.Utility.Logs;
using UnityEngine.SceneManagement;
using Zenject;

namespace LetMeOut.Bootstrapper
{
    public class BootstrapperInstaller : MonoInstaller
    {
        private const string SCENE_NAME = "Main";

        public override void InstallBindings()
        {
            Container.Bind<ILog>().FromInstance(new UnityLogger());
        }

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name.Equals(SCENE_NAME) == false)
            {
                SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Additive);
            }
        }
    }
}