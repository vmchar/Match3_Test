using UnityEngine;
using System.Collections.Generic;
using System;

namespace Match3Test
{
    [Serializable]
    public class LevelModel
    {
        /// <summary>
        /// Should element be generated all together at level start, or field should be
        /// generated and elements will be generated sequentially and fall from emitters
        /// </summary>
        public bool PrefilledLevel;

        public GameObject ElementPrefab;

        [SerializeField]
        public List<ElementTemplateModel> ElementModels;
    }

}