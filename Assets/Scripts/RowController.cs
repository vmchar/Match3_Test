using UnityEngine;
using System.Collections.Generic;

namespace Match3Test
{
    public class RowController : MonoBehaviour
    {
        #region Public Properties

        public List<TileController> Tiles { get { return _tiles; }}

        public int YDimention { get { return _yDimention; } }

        public Transform ElementEmitterTransform{ get { return _elementEmitterTransform; } }

        public Transform RowTransform 
        {
            get 
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        /// <summary>
        /// Determines if all of the row's tiles have Element.
        /// </summary>
        /// <value><c>true</c> if is full; otherwise, <c>false</c>.</value>
        public bool IsFull 
        {
            get 
            {
                for (var i = 0; i < Tiles.Count; i++) 
                {
                    if (Tiles [i].Element == null)
                        return false;
                }
                return true;
            }
        }

        #endregion

        #region Private Variables

        private Transform _transform;
        private Transform _elementEmitterTransform;
        private int _yDimention;
        [SerializeField]
        private List<TileController> _tiles;

        #endregion

        #region Public Methods

        public void Initialize (int dimention, List<TileController> tiles, Transform elementEmitter)
        {
            _yDimention = dimention;
            _tiles = tiles;
            _elementEmitterTransform = elementEmitter;
        }

        #endregion
    }
}