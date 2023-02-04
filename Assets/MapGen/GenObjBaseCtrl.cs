using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenObjBaseCtrl : ScriptableObject
{
    public GameObject Prefeb;
    public abstract bool CanGen(Dictionary<Vector2Int, Type> data, Vector2Int chunk);
}

public class ObjFactory
{
    private GenObjBaseCtrl Data;
    private ObjPoolCtrl<ObjBaseCtrl> Pool;
    private Transform GenRoot;
    
    public ObjFactory(GenObjBaseCtrl data,Transform genRoot)
    {
        Data = data;
        GenRoot = genRoot;
        Pool = new ObjPoolCtrl<ObjBaseCtrl>(() => MonoBehaviour.Instantiate(Data.Prefeb, GenRoot));
    }

    public PoolObj<ObjBaseCtrl> ForceGen(Vector2Int chunk)
    {
        var o = Pool.Get();
        o.Obj.transform.position = Define.Chunk2Pos(chunk);
        return o;
    }
    
    public bool CheckCanGen(Dictionary<Vector2Int, Type> data, Vector2Int chunk, out PoolObj<ObjBaseCtrl> obj)
    {
        obj = null;
        if (!Data.CanGen(data, chunk)) return false;
        obj = ForceGen(chunk);
        return true;
    }
}
