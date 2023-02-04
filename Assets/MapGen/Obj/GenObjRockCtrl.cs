using System;
using System.Collections;
using System.Collections.Generic;
using Game.GameEvent;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Rock", menuName = "GameJam2023/GenObj")]
public class GenObjRockCtrl : GenObjBaseCtrl
{
    public int GenDistance = 7;
    public float RandomSkip = 0.5f;
    public float MinCanGenY = 1;
    
    public override bool CanGen(Dictionary<Vector2Int, Type> data, Vector2Int chunk)
    {
        var pos = Define.Chunk2Pos(chunk);
        if (pos.y < MinCanGenY) return false;
        var space = new Vector3(GenDistance , GenDistance , 0);
        var chuncks = Define.InAreaChunk(pos - space, pos + space);
        foreach (var chunck in chuncks) {
            if (data.ContainsKey(chunck)) return false;
        }
        if(Random.value<RandomSkip)return false;
        return true;
    }
}
