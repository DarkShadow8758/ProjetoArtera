using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonScript : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private Canvas optionsMenuCanvas;
    [SerializeField] private Button optionsButton;
    
    private bool isMenuOpen = false;
    
    void Start()
    {
        // Certifica que o menu começa fechado
        if (optionsMenuCanvas != null)
        {
            optionsMenuCanvas.sortingOrder = -1;
            isMenuOpen = false;
        }
        
        // Adiciona o listener do botão
        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(ToggleOptionsMenu);
        }
        else
        {
            // Se não foi atribuído no Inspector, tenta pegar o Button do próprio objeto
            optionsButton = GetComponent<Button>();
            if (optionsButton != null)
            {
                optionsButton.onClick.AddListener(ToggleOptionsMenu);
            }
        }
    }
    
    public void ToggleOptionsMenu()
    {
        if (optionsMenuCanvas == null)
        {
            Debug.LogWarning("Canvas do menu de opções não foi atribuído!");
            return;
        }
        
        // Alterna entre aberto e fechado
        isMenuOpen = !isMenuOpen;
        
        if (isMenuOpen)
        {
            // Menu aberto - Sort Order = 1
            optionsMenuCanvas.sortingOrder = 1;
            Debug.Log("Menu de opções aberto");
        }
        else
        {
            // Menu fechado - Sort Order = -1
            optionsMenuCanvas.sortingOrder = -1;
            Debug.Log("Menu de opções fechado");
        }
    }
    
    // Método público para fechar o menu (útil para outros scripts)
    public void CloseOptionsMenu()
    {
        if (optionsMenuCanvas != null)
        {
            optionsMenuCanvas.sortingOrder = -1;
            isMenuOpen = false;
        }
    }
    
    // Método público para abrir o menu (útil para outros scripts)
    public void OpenOptionsMenu()
    {
        if (optionsMenuCanvas != null)
        {
            optionsMenuCanvas.sortingOrder = 1;
            isMenuOpen = true;
        }
    }
    
    // Propriedade para verificar se o menu está aberto
    public bool IsMenuOpen
    {
        get { return isMenuOpen; }
    }
}