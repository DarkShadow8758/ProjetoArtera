using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;  
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private FirstPersonController playerController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (playerController.nextScene)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(1f);
        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
