using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjBaseCtrl : MonoBehaviour
{
    public bool IsHooking;
    public List<IDisposable> EventHooks;

    private void Awake()
    {
        EventHooks = new List<IDisposable>();

    }

    private void OnEnable()
    {
        EventHook();
        void EventHook()
        {
            EventHooks.Add(            
                EventAggregator.OnEvent<HookRock>().Subscribe(e => {
                if (e.O == gameObject)
                {
                    if (!IsHooking)
                    {
                        IsHooking = true;
                        HookOn();
                    }
                }
                else
                {
                    if (IsHooking)
                    {
                        IsHooking = false;
                        HookOff();
                    }
                }
            }));
        }
    }

    private void OnDisable()
    {
        EventHooks.ForEach(e=>e.Dispose());
        EventHooks.Clear();
    }

    public virtual void HookOn()
    {
        Debug.Log("HookOn!");
    }
    
    public virtual void HookOff()
    {
        Debug.Log("HookOff!");
    }
}
