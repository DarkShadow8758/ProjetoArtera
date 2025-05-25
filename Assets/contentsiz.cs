using UnityEngine;
using TMPro;

public class OptionsBtn : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText; // Arraste seu TextMeshPro aqui pelo Inspector

    // Chame este método no evento OnClick do seu botão
    public void ShowGameInfo()
    {
        infoText.text =
            "Controles:\n" +
            "- W, A, S, D: Movimentação\n" +
            "- Ctrl: Agachar\n" +
            "- Shift: Correr\n" +
            "- Shift + Ctrl: Deslizar\n" +
            "- Mouse Esq: Tirar foto\n" +
            "- Q: Acessar quadro de missões\n" +
            "- E: Interagir com missões e NPCs\n" +
            "- Espaço/Enter: Passar diálogo\n" +
            "- Mouse: Olhar\n\n" +
            "Sobre o jogo:\n"
            + "É um jogo educativo de aventura que explora os movimentos artísticos da história, "
            + "começando pelo Cubismo. A protagonista, sem saber ao certo onde está, desperta "
            + "em um mundo estranho e fragmentado. Ao longo de sua jornada, ela viaja por diferentes "
            + "cenários inspirados em movimentos artísticos, descobrindo suas características únicas e "
            + "registrando obras icônicas com sua câmera. Cada foto tirada ajuda a reconstruir uma "
            + "galeria de arte interativa, revelando aos poucos o contexto e a importância de cada estilo. "
            + "Com uma narrativa envolvente e visual estilizado, "
            + "Artera convida o jogador a aprender arte de forma lúdica e imersiva.";
    }
}
