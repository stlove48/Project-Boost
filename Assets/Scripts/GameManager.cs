using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;
    int mainMenuIndex = 0;
    int firstLevelIndex = 1;
    [SerializeField] GameObject scoreCanvas;

    
    
    private void Awake()
    {
        if (gmInstance != null && gmInstance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            gmInstance = this;
        }

        DontDestroyOnLoad(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void QuitGame() 
    {
        Debug.Log("Quit application");
            Application.Quit();
    }

    public void LoadFirstLevel() 
    {
        SceneManager.LoadScene(firstLevelIndex);
        scoreCanvas.SetActive(true);
    }
}
