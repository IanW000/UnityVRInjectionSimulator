using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]FadeScreen fadeScreen;
    // Start is called before the first frame update
    void Start()
    {
    }

    

    public void pressReset()
    {
        StartCoroutine(restartScene());
    }

    IEnumerator restartScene()
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
