using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Certifique-se de que este GameObject tem um Collider marcado como "Is Trigger"

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtém o índice da cena atual
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Calcula o índice da próxima cena (atual + 1)
            int nextSceneIndex = currentSceneIndex + 1;

            // Verifica se a próxima cena existe no build
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Carrega a próxima cena
                SceneManager.LoadScene(nextSceneIndex);
                Debug.Log("Mudando para a cena: " + nextSceneIndex);
            }
            else
            {
                // Se não existir próxima cena, você pode voltar para a primeira
                SceneManager.LoadScene(0);
                Debug.Log("Voltando para a primeira cena");
            }
        }
    }
}