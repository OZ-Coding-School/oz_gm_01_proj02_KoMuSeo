using UnityEngine;

public class UIInputs : MonoBehaviour
{
    [SerializeField] UIManager um;
    InputSystem_Actions inputActions;

    private void Awake()
    {
        um = GetComponent<UIManager>();
        inputActions = new InputSystem_Actions();

        inputActions.UI.ESC.started += _ => um.Show(UIKey.Pause, true);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}