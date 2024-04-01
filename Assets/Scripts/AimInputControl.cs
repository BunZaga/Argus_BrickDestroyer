using UnityEngine;
using UnityEngine.UI;

public class AimInputControl : MonoBehaviour
{
    public static GameEvent evtOnMouseUp = new GameEvent();
    public static GameEvent evtOnMouseCancel = new GameEvent();

    public static Vector3 InputDirection;
    public static Vector3 MousePosition;
    public static bool CanFire = false;
    private bool canFire;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform dragRoot;
    [SerializeField] private Transform dragAbove;
    [SerializeField] private GameObject dragIndicator;
    [SerializeField] private Color canFireColor;
    [SerializeField] private Color cannotFireColor;
    [SerializeField] private Material dragIndicatorMat;
    [SerializeField] private GraphicRaycaster raycaster;
  
    private Vector3 screenPoint;

    private void Awake()
    {
        raycaster.enabled = false;
        GameGrid.EvtGameStart.AddListener(OnGameStart);
        GameGrid.EvtGameOver.AddListener(OnGameOver);
        GameGrid.EvtLoadNewLevel.AddListener(OnLoadNewLevel);
        GameGrid.EvtOpenSwapAmmo.AddListener(OnSwapAmmoShow);
        GameGrid.EvtCloseSwapAmmo.AddListener(OnSwapAmmoHide);
    }
    
    private void OnGameStart()
    {
        raycaster.enabled = true;
    }

    private void OnGameOver()
    {
        raycaster.enabled = false;
    }

    private void OnLoadNewLevel()
    {
        raycaster.enabled = true;
    }
    
    private void Update()
    {
        if (!GameGrid.GameInPlay)
            return;
        
        UpdateAimInput();
    }

    private void OnSwapAmmoShow()
    {
        raycaster.enabled = false;
    }
    
    private void OnSwapAmmoHide()
    {
        raycaster.enabled = true;
    }
    
    private void UpdateAimInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            evtOnMouseUp.Invoke();
        }
        screenPoint.x = Input.mousePosition.x;
        screenPoint.y = Input.mousePosition.y;
        screenPoint.z = 50.0f;

        MousePosition = cam.ScreenToWorldPoint(screenPoint);
        InputDirection = (MousePosition - dragRoot.position).normalized;
        
        CanFire = false;
        if(MousePosition.y > dragAbove.position.y)
        {
            CanFire = true;
            if (!canFire)
            {
                canFire = true;
                dragIndicatorMat.color = canFireColor;
            }
        }
        if(canFire && !CanFire)
        {
            canFire = false;
            dragIndicatorMat.color = cannotFireColor;
            evtOnMouseCancel.Invoke();
        }
        dragIndicator.transform.position = MousePosition;
    }
}

