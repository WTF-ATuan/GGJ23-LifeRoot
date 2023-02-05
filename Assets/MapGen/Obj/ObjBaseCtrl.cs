using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ObjBaseCtrl : MonoBehaviour
{
    public static ObjBaseCtrl HookingObj { private set; get; }

    public UnityEvent OnHookOn;
    
    protected bool IsHooking;
    private List<IDisposable> EventHooks;
    public Vector2Int MyChunk { private set; get; }
    
    protected virtual void Awake()
    {
        EventHooks = new List<IDisposable>();

    }

    protected virtual void OnEnable()
    {

        EventHook();
        SetChunk();
        
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

        async Task SetChunk()
        {
            await Task.Delay(10);
            MyChunk = Define.Pos2Chunk(transform.position);
        }
    }

    private void OnDisable()
    {
        EventHooks.ForEach(e=>e.Dispose());
        EventHooks.Clear();
    }

    public virtual void HookOn()
    {
        HookingObj = this;
        OnHookOn.Invoke();
    }
    
    public virtual void HookOff()
    {
        HookingObj = null;
    }

    public ObjType GetType()
    {
        return MapGenCtrl.Instance.GetChunkType(MyChunk);
    }

    public void Recycle()
    {
        MapGenCtrl.Instance.RecycleChunk(MyChunk);
    }
}
