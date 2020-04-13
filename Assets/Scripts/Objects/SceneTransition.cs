using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Info")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue positionStorage;

    [Header("Fade")]
    public GameObject fadePanel;
    public float fadeWait;

    void Awake()
    {
        Fade(true);
    }

    void Fade(bool fadeIn)
    {
        if (fadePanel != null)
        {
            var panel = Instantiate(fadePanel, Vector3.zero, Quaternion.identity);
            var anim = panel.GetComponentInChildren<Animator>();
            if (fadeIn)
            {
                anim.SetBool("fadeIn", true);
            }
            else
            {
                anim.SetBool("fadeOut", true);
            }

            Destroy(panel, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            positionStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    IEnumerator FadeCo()
    {
        AsyncOperation op;

        if (fadePanel != null)
        {
            Fade(false);
            op = SceneManager.LoadSceneAsync(sceneToLoad);
            yield return new WaitForSeconds(fadeWait);
        }
        else
        {
            op = SceneManager.LoadSceneAsync(sceneToLoad);
        }

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
