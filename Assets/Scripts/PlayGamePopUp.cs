using UnityEngine;
using UnityEngine.UI;

public class PlayGamePopUp : MonoBehaviour
{
    [SerializeField] private Button playGame;

    public void Awake()
    {
        playGame.onClick.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        GameGrid.EvtGameStart.Invoke();
    }
}
