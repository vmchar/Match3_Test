using UnityEngine;
using System.Collections.Generic;

namespace Match3Test
{
    public abstract class BaseElementModel
    {
        #region Public Properties

        public Sprite NormalSprite { get { return _normalSprite; } }
        public Sprite SelectedSprite { get { return _selectedSprite; } }
        public ElementType Type { get { return _type; } }
        public BaseMathchingStategy Strategy { get { return _matchingStrategy; } }
        public List<ElementType> MatchingTypes { get { return _machingTypes; } }

        #endregion

        #region Protected Variables

        protected readonly Sprite _normalSprite;
        protected readonly Sprite _selectedSprite;
        protected readonly ElementType _type;
        protected List<ElementType> _machingTypes = new List<ElementType> ();
        protected BaseMathchingStategy _matchingStrategy;

        #endregion

        #region Protected Methods

        protected BaseElementModel (ElementType type, Sprite normalSprite, Sprite selectedSprite)
        {
            _type = type;
            _normalSprite = normalSprite;
            _selectedSprite = selectedSprite;
        }

        #endregion
    }
}