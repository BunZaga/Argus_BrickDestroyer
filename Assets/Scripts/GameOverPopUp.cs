using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button exit;
    [SerializeField] private Button replay;

    private void Awake()
    {
        nextLevel.onClick.AddListener(OnButtonNextLevel);
        exit.onClick.AddListener(OnButtonExit);
        replay.onClick.AddListener(OnButtonReplay);
    }
    
    private void OnButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnButtonNextLevel()
    {
        GameGrid.Instance.LoadNextLevel();
    }

    private void OnButtonReplay()
    {
        GameGrid.Instance.LoadLevel(-1);
    }

    private void OnDestroy()
    {
        nextLevel.onClick.RemoveListener(OnButtonNextLevel);
        exit.onClick.RemoveListener(OnButtonExit);
        replay.onClick.RemoveListener(OnButtonReplay);
    }
}
