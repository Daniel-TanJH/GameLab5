using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneEV : MonoBehaviour
{
    public AudioSource changeSceneSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(ChangeScene("MarioLevel2"));
        }
    }

    IEnumerator WaitSoundClip(string sceneName)
    {
        
        StartCoroutine(ChangeScene("MarioLevel2"));
        yield return null;
    }
    IEnumerator ChangeScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}