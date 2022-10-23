using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]FadeScreen fadeScreen;

    public void pressReset()
    {
        StartCoroutine(restartSceneImmediately());
    }

    public void gameEnd(int seconds)
    {
        StartCoroutine(restartSceneAfterFewSeconds(seconds));
    }

    IEnumerator restartSceneImmediately()
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator restartSceneAfterFewSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
