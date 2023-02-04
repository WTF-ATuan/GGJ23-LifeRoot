using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public const float ChunkSize = 10;
    public const float GenRange = 5;

    public static Vector2Int Pos2Chunk(Vector3 pos)
    {
        pos /= ChunkSize;
        return new Vector2Int((int)Mathf.Ceil(pos.x), (int)Mathf.Ceil(pos.y));
    }

    public static Vector3 Chunk2Pos(Vector2Int chunk)
    {
        var pos = new Vector3(chunk.x*ChunkSize, chunk.y*ChunkSize, 0);
        return pos;
    }

    public static List<Vector2Int> InAreaChunk(Vector2 ldPos,Vector2 trPos)
    {
        var chunks = new List<Vector2Int>();

        Vector2Int leftDown = Pos2Chunk(ldPos);
        Vector2Int rightTop = Pos2Chunk(trPos);

        for (int i = leftDown.x; i <= rightTop.x; i++)
        {
            for (int j = leftDown.y; j <= rightTop.y; j++)
            {
                chunks.Add(new Vector2Int(i, j));
            }
        }

        return chunks;
    }
}
