using System;
using System.Collections;
using System.Collections.Generic;
using Game.GameEvent;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Rock", menuName = "GameJam2023/GenObj")]
public class GenObjRockCtrl : GenObjBaseCtrl
{
    public int GenDis_SameType = 7;
    public int GenDis_Others = 8;
    public float GenChance = 0.5f;
    public float MinCanGenY = 1;
    public float MaxCanGenY = 9999;
    // public float lifetime
    
    
    public override bool CanGen(Dictionary<Vector2Int, ObjType> data, Vector2Int chunk)
    {
        var pos = Define.Chunk2Pos(chunk);
        if (pos.y < MinCanGenY) return false;
        if (pos.y>MaxCanGenY) return false;
        var space = new Vector3(GenDis_SameType , GenDis_SameType , 0);
        var chuncks = Define.InAreaChunk(pos - space, pos + space);
        foreach (var chunck in chuncks) {
            if (data.ContainsKey(chunck) && data[chunck]==Type) return false;
        }

        space = new Vector3(GenDis_Others , GenDis_Others , 0);
        chuncks = Define.InAreaChunk(pos - space, pos + space);
        foreach (var chunck in chuncks) {
            if (data.ContainsKey(chunck) && data[chunck]!=ObjType.Null) return false;
        }
        
        if(Random.value>GenChance)return false;
        return true;
    }
}

public enum ObjType
{
    Null,
    Rock,
    Rocket,
    BreakRock,
    DashBird,
    BirdNest,
    RockHigh,
    Rainbow,
}

