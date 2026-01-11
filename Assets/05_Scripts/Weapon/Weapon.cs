using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FireMode
{
    Single,
    SemiAuto,
    Auto
}

public abstract class Weapon : MonoBehaviour
{
    protected WeaponContext context;
    protected IWeaponFireStrategy fireStrategy;
    protected Dictionary<FireMode, IFireModeStrategy> fireModes = new();
    [SerializeField] protected FireMode currentMode;
    FireMode[] modes;

    protected virtual void Awake()
    {
        context = new();
        fireStrategy = new HybridFireStrategy();
        modes = Enum.GetValues(typeof(FireMode)).Cast<FireMode>().ToArray();
    }

    protected virtual void Start()
    {
        context.dms = StaticRegistry.Find<DamageSystem>();
    }

    public void SetFireMode(FireMode mode)
    {
        if (!fireModes.ContainsKey(mode)) return;
        currentMode = mode;
    }

    public void NextFireMode()
    {
        int startIdx = Array.IndexOf(modes, currentMode);

        for (int i = 1; i <= modes.Length; ++i)
        {
            FireMode next = modes[(startIdx + i) % modes.Length];
            if (fireModes.ContainsKey(next))
            {
                currentMode = next;
                return;
            }
        }
    }

    public virtual void Fire(FireInputContext input)
    {
        fireModes[currentMode].Tick(this, context, input);
    }

    public virtual void DoFire(WeaponContext context)
    {
        fireStrategy.Fire(context);
    }
}