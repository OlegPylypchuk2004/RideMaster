using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace SceneLoadingSystem
{
    public class SceneLoader
    {
        private readonly float _loadDelay;

        public event Action LoadStarted;
        public event Action LoadCompleted;

        public SceneLoader(float loadDelay)
        {
            _loadDelay = loadDelay;
        }

        public int ActiveSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public string ActiveSceneName => SceneManager.GetActiveScene().name;

        public void Load(int index)
        {
            LoadAsync(index)
                .Forget();
        }

        public void Load(string name)
        {
            LoadAsync(name)
                .Forget();
        }

        private async UniTaskVoid LoadAsync(int index)
        {
            LoadStarted?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(_loadDelay));

            await SceneManager.LoadSceneAsync(index)
                .ToUniTask();

            LoadCompleted?.Invoke();
        }

        private async UniTaskVoid LoadAsync(string name)
        {
            LoadStarted?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(_loadDelay));

            await SceneManager.LoadSceneAsync(name)
                .ToUniTask();

            LoadCompleted?.Invoke();
        }
    }
}