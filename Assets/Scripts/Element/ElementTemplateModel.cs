using UnityEngine;
using System;

namespace Match3Test
{
    [Serializable]
    public class ElementTemplateModel
    {
        public Sprite NormalSprite;
        public Sprite SelectedSprite;
        public ElementType Type;
        public bool Generatable;
    }
}
