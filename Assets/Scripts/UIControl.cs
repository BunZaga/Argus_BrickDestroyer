using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private GameObject playGamePopUp;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private GameObject swapAmmoPopUp;

    public void Awake()
    {
        GameGrid.EvtGameStart.AddListener(CloseAllDisableRaycaster);
        GameGrid.EvtGameOver.AddListener(OnGameOver);
        GameGrid.EvtLoadNewLevel.AddListener(CloseAllDisableRaycaster);
        GameGrid.EvtCloseSwapAmmo.AddListener(CloseAllDisableRaycaster);
        GameGrid.EvtOpenSwapAmmo.AddListener(OnSwapAmmo);
    }
    
    private void Start()
    {
        raycaster.enabled = true;
        playGamePopUp.SetActive(true);
        gameOverPopUp.SetActive(false);
        swapAmmoPopUp.SetActive(false);
    }

    private void CloseAllDisableRaycaster()
    {
        raycaster.enabled = false;
        playGamePopUp.SetActive(false);
        gameOverPopUp.SetActive(false);
        swapAmmoPopUp.SetActive(false);
    }
    
    private void OnGameOver()
    {
        raycaster.enabled = true;
        playGamePopUp.SetActive(false);
        gameOverPopUp.SetActive(true);
        swapAmmoPopUp.SetActive(false);
    }

    private void OnSwapAmmo()
    {
        raycaster.enabled = true;
        playGamePopUp.SetActive(false);
        gameOverPopUp.SetActive(false);
        swapAmmoPopUp.SetActive(true);
    }
}
