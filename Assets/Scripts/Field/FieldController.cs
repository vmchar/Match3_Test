using UnityEngine;
using System.Collections.Generic;

namespace Match3Test
{
    public class FieldController : MonoBehaviour
    {
        #region Public Properties

        public List<RowController> Rows { get { return _rows; }}

        public Transform FieldTransform
        {
            get 
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        #endregion

        #region Private Variables

        private Transform _transform;
        private List<RowController> _rows;

        #endregion

        #region Public Methods

        public void Initialize ( List<RowController> rows)
        {
            _rows = rows;
        }

        #endregion
    }

}