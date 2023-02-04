using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapGenCtrl : MonoBehaviour
{
    public Transform GenRoot;
    public List<GenObjBaseCtrl> GenObjList;


    private Dictionary<Vector2Int, Type> Data;
    private Dictionary<Vector2Int, PoolObj<ObjBaseCtrl>> Objs;
    private Dictionary<Type, ObjFactory> ObjFactories;
    
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
        GenObjInit();
        ChunkInit();
        
        void GenObjInit()
        {
            Data = new Dictionary<Vector2Int, Type>();
            Objs = new Dictionary<Vector2Int, PoolObj<ObjBaseCtrl>>();
            ObjFactories = new Dictionary<Type, ObjFactory>();
            
            foreach (var objBaseCtrl in GenObjList) {
                ObjFactories.Add(objBaseCtrl.GetType(), new ObjFactory(objBaseCtrl, GenRoot));
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

    void RecycleChunk(Vector2Int chunk)
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
                foreach (var factory in ObjFactories) {
                    if (factory.Value.CheckCanGen(Data, chunk, out obj)) {
                        Data.Add(chunk, factory.Key);
                        break;
                    }
                }
            } else {
                obj = ObjFactories[Data[chunk]].ForceGen(chunk);
            }
            
            if (obj == null) return;
            Objs.Add(chunk, obj);
        }
    }
    
    /*
    void YUpdate()
    {
        CameraUpdate();
        BGUpdate();
        ObjUpdate();

        void CameraUpdate()
        {
            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.y = LastY;
            Camera.main.transform.position = cameraPos;
        }

        void BGUpdate() {
            BG.material.SetFloat("_Offset",LastY);
        }

        void ObjUpdate() {
            foreach (var genObjEntitiy in GenObjEntities) {
                genObjEntitiy.OnUpdate(ref GenObjDatas, LastY);
            }
        }
        
    }
    */
}
