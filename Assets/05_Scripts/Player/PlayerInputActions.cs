using System.Collections;
using UnityEngine;

public class PlayerInputActions : MonoBehaviour
{
    public InputSystem_Actions InputActions { get; private set; }
    public PlayerController pctrl;

    GameManager gm;

    private void Awake()
    {
        InputActions = new InputSystem_Actions();
        pctrl = GetComponent<PlayerController>();

        InputActions.Player.Move.performed += pctrl.OnMoveInput;
        InputActions.Player.Move.canceled  += pctrl.OnMoveInputCanceled;

        InputActions.Player.Sprint.performed += pctrl.OnSprintInput;
        InputActions.Player.Sprint.canceled  += pctrl.OnSprintInputCanceled;

        InputActions.Player.Jump.started += pctrl.OnJumpInput;

        InputActions.Player.Crouch.performed += pctrl.OnCrouchInput;
        InputActions.Player.Crouch.canceled  += pctrl.OnCrouchInputCanceled;

        InputActions.Player.Attack.performed += pctrl.OnFireInput;
        InputActions.Player.Attack.canceled  += pctrl.OnFireInputCanceled;

        InputActions.Player.Previous.performed += pctrl.OnMainWeaponInput;

        InputActions.Player.Next.performed += pctrl.OnSubWeaponInput;

        InputActions.Player.Grenade.performed += pctrl.OnGrenadeInput;

        InputActions.Player.FireModeChange.performed += pctrl.OnModeInput;

        InputActions.Player.Melee.started += pctrl.OnMeleeInput;
        InputActions.Player.Melee.canceled += pctrl.OnMeleeInputCanceled;

        InputActions.Player.Reload.started += pctrl.OnReloadInput;

    }

    private void OnEnable()
    {
        StartCoroutine(Co_BindGameManager());
    }

    private void OnDisable()
    {
        if (gm) gm.OnPlayStateChanged -= OnPlayStateChanged;
        InputActions.Disable();
    }

    IEnumerator Co_BindGameManager()
    {
        if (gm) yield break;

        while ((gm = StaticRegistry.Find<GameManager>()) == null)
            yield return null;

        gm.OnPlayStateChanged += OnPlayStateChanged;
        ApplyInputEnable();
    }

    void OnPlayStateChanged(PlayState _)
    {
        ApplyInputEnable();
    }

    void ApplyInputEnable()
    {
        bool enable = gm && gm.IsPlaying;
        if (enable)
            InputActions.Player.Enable();
        else 
            InputActions.Player.Disable();
    }
}