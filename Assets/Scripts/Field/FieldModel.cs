using UnityEngine;
using System;

namespace Match3Test
{
    [Serializable]
    public class FieldModel
    {
        public int Xdimention;
        public int Ydimention;

        public float TileXSize;
        public float TileYSize;

        public GameObject TilePrefab;
        public GameObject RowPrefab;
        public GameObject ElementEmitterPrefab;
    }
}