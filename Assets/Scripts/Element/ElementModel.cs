using UnityEngine;
using System;

namespace Match3Test
{
    [Serializable]
    public class ElementModel
    {
        #region Public Properties

        public Sprite NormalSprite { get { return _normalSprite;} } 
        public Sprite SelectedSprite { get { return _selectedSprite; } }
        public ElementType Type { get { return _type; } }

        #endregion

        #region Private Variables

        private readonly Sprite _normalSprite;
        private readonly Sprite _selectedSprite;
        private readonly ElementType _type;

        #endregion

        #region Public Methods

        public ElementModel (ElementType type, Sprite normalSprite, Sprite selectedSprite)
        {
            _type = type;
            _normalSprite = normalSprite;
            _selectedSprite = selectedSprite;
        }

        #endregion
    }
}
