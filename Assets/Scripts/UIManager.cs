using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject instructionPanel;
    public GameObject winPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowInstructions();
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void HideInstructions()
    {
        instructionPanel.SetActive(false);
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
