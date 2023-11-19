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
            BindLogService();
        }

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name.Equals(SCENE_NAME) == false)
            {
                SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Additive);
            }
        }

        private void BindLogService()
        {
            Container.Bind<ILogService>().FromInstance(new UnityLogger());
        }
    }
}