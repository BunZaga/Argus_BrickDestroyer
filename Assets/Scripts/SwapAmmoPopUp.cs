using UnityEngine;
using UnityEngine.UI;

public class SwapAmmoPopUp : MonoBehaviour
{
    [SerializeField] private Button btnOk;

    private void Awake()
    {
        btnOk.onClick.AddListener(OnButtonOk);
    }

    private void OnButtonOk()
    {
        GameGrid.EvtCloseSwapAmmo.Invoke();
        gameObject.SetActive(false);
    }
}
