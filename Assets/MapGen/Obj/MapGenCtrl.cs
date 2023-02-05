using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class MapGenCtrl : MonoBehaviour
{
    public static MapGenCtrl Instance;
    
    public Transform GenRoot;
    public List<GenObjBaseCtrl> GenObjList;
    
    private Dictionary<Vector2Int, ObjType> Data;
    private Dictionary<Vector2Int, PoolObj<ObjBaseCtrl>> Objs;
    private Dictionary<ObjType, ObjFactory> ObjFactories;
    
    private Vector2Int LastChunk;
    private List<Vector2Int> LastInCameraChunks;

    private List<Vector2Int> GetInAreaChunk()
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 offset = new Vector3(Define.GenRange, Define.GenRange, 0);
        bottomLeft -= offset;
        topRight += offset;
        
        return Define.InAreaChunk(bottomLeft, topRight);
    }
    
    private void Awake()
    {
        Instance = this;
        
        GenObjInit();
        ChunkInit();
        
        void GenObjInit()
        {
            Data = new Dictionary<Vector2Int, ObjType>();
            Objs = new Dictionary<Vector2Int, PoolObj<ObjBaseCtrl>>();
            ObjFactories = new Dictionary<ObjType, ObjFactory>();

            foreach (var objBaseCtrl in GenObjList){
                ObjFactories.Add(objBaseCtrl.Type , new ObjFactory(objBaseCtrl, GenRoot));
            }
        }
        
        void ChunkInit()
        {
            LastInCameraChunks = GetInAreaChunk();
            LastInCameraChunks.ForEach(e=>GenChunk(e));
        }
    }

    private void Update()
    {
        ChunkUpdate();
        
        void ChunkUpdate()
        {
            var nowChunk = Define.Pos2Chunk(Camera.main.transform.position);
            if (LastChunk != nowChunk)
            {
                LastChunk = nowChunk;
                var nowInAreaChunk = GetInAreaChunk();

                var needRecycle = LastInCameraChunks.Where(e => !nowInAreaChunk.Contains(e)).ToList();
                var needGen = nowInAreaChunk.Where(e => !LastInCameraChunks.Contains(e)).ToList();
            
                needRecycle.ForEach(e=>RecycleChunk(e));
                needGen.ForEach(e=>GenChunk(e));
            
                LastInCameraChunks = nowInAreaChunk;
            }
        }
    }

    public void RecycleChunk(Vector2Int chunk)
    {
        bool haveObj = Objs.ContainsKey(chunk);
        if (!haveObj) return;
        Objs[chunk].Dispose();
        Objs.Remove(chunk);
    }
    
    void GenChunk(Vector2Int chunk)
    {
        bool haveData = Data.ContainsKey(chunk);
        bool haveObj = Objs.ContainsKey(chunk);
        if (!haveData) {
            if (haveObj) {
                //should't run this line!
                RecycleChunk(chunk);
            }
            else
            {
                GenObj(chunk);
            }
        }else {
            if (!haveObj) {
                GenObj(chunk);
            }
        }

        void GenObj(Vector2Int chunk)
        {
            RecycleChunk(chunk);
       
            PoolObj<ObjBaseCtrl> obj = null;
            bool haveData = Data.ContainsKey(chunk);
            if (!haveData) {
                ObjType t = ObjType.Null; 
                foreach (var factory in ObjFactories) {
                    if (factory.Value.CheckCanGen(Data, chunk, out obj)) {
                        t = factory.Key;
                        break;
                    }
                }
                Data.Add(chunk, t);
            } else {
                if (ObjFactories.ContainsKey(Data[chunk])) {
                    obj = ObjFactories[Data[chunk]].ForceGen(chunk);
                }
            }
            
            if (obj == null) return;
            Objs.Add(chunk, obj);
        }
    }

    public void ChangeChunkType(Vector2Int chunk, ObjType type)
    {
        if (Data.ContainsKey(chunk)) {
            Data[chunk] = type;
        } else {
            Data.Add(chunk, type);
        }
    }

    public ObjType GetChunkType(Vector2Int chunk)
    {
        if (Data.ContainsKey(chunk))
        {
            return Data[chunk];
        } 
        return ObjType.Null;
    }
}
