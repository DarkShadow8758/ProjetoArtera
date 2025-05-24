using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Função que será chamada quando o botão for clicado
    public void SairDoJogo()
    {
        // No editor do Unity (para testar)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Na build final do jogo
            Application.Quit();
#endif
    }
}