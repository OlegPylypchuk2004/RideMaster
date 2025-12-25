using SceneLoadingSystem;
using System.Collections;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private IEnumerator Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;

        yield return new WaitForSeconds(1f);

        _sceneLoader.Load(_sceneLoader.ActiveSceneIndex + 1);
    }
}