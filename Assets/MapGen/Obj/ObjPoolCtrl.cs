using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolCtrl<T>where T : Component
{
    public delegate GameObject GetGameObject();
    private GetGameObject CreateFunc;
    private List<GameObject> ActivePool;
    private List<GameObject> UnActivePool;
    private List<PoolObj<T>> ActivePoolObjs;
    private List<int> EveageActiveCount;
    private int ActivePoolSize;
    private int MinPoolSize;
    private int PoolSize => ActivePoolSize < MinPoolSize ? MinPoolSize : ActivePoolSize;
    
    public ObjPoolCtrl(GetGameObject createFunc, int minSize = 1)
    {
        CreateFunc = createFunc;
        MinPoolSize = minSize;
        ActivePool = new List<GameObject>();
        UnActivePool = new List<GameObject>();
        ActivePoolObjs = new List<PoolObj<T>>();
        EveageActiveCount = new List<int>();
    }

    public List<PoolObj<T>> GetAllActiveObj()
    {
        return new List<PoolObj<T>>(ActivePoolObjs);
    }

    public void Clean()
    {
        GetAllActiveObj().ForEach(e=>e.Dispose());
    }
    
    public PoolObj<T> Get()
    {
        GameObject o;
        if (UnActivePool.Count == 0) {
            o = Create();
            o.SetActive(true);
        } else {
            o = UnActivePool[0];
            UnActivePool.Remove(o);
            ActivePool.Add(o);
            o.transform.SetAsLastSibling();
            o.SetActive(true);
        }

        Cheak();
        var obj = new PoolObj<T>(o, (e) => {
            o.SetActive(false);
            ActivePool.Remove(o);
            ActivePoolObjs.Remove(e);
            Cheak();
            
            if(UnActivePool.Count<PoolSize)
                UnActivePool.Add(o);
            else 
                GameObject.Destroy(o);
        });
        ActivePoolObjs.Add(obj);
        return obj;
    }

    GameObject Create() {
        var o = CreateFunc();
        ActivePool.Add(o);
        return o;
    }

    void Cheak()
    {
        EveageActiveCount.Add(ActivePool.Count);
        if(EveageActiveCount.Count>10)EveageActiveCount.RemoveAt(0);
        float all = 0;
        EveageActiveCount.ForEach(e=>all+=e);
        all /= EveageActiveCount.Count;
        ActivePoolSize = Mathf.FloorToInt(all) + 1;
    }
    
}

public class PoolObj<T>:IDisposable where T : Component{
    
    public GameObject Obj;
    public T Ctrl;
    private Action<PoolObj<T>> OnDispose;
    
    public PoolObj(GameObject obj,Action<PoolObj<T>> onDispose) {
        Obj = obj;
        Ctrl = Obj.GetComponent<T>();
        OnDispose = onDispose;
    }
    
    public void Dispose() {
        OnDispose.Invoke(this);
    }
}
