using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rock", menuName = "GameJam2023/GenObj")]
public class GenObjRockCtrl : GenObjBaseCtrl
{
    public override bool CanGen(Dictionary<Vector2Int, Type> data, Vector2Int chunk)
    {
        return true;
    }
}
