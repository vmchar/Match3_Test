using UnityEngine;

namespace Match3Test
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class TileController : MonoBehaviour
    {
        #region Public Variables

        public Transform TileTransform;
        public SpriteRenderer Renderer;

        #endregion

        #region Public Properties

        public bool IsEmpty { get { return Element == null; } }

        public ElementController Element 
        {
            get { return _element; }
            set { _element = value; }
        }

        #endregion

        #region PrivateVariables

        private ElementController _element;

        #endregion

        #region MonoBehaviour Methods

        private void Start ()
        {
            if (TileTransform == null) TileTransform = transform;
            if (Renderer == null) Renderer = GetComponent<SpriteRenderer> ();
        }

        #endregion

        #region Public Methods

        public void DestroyElement ()
        {
            if (IsEmpty) return;
            Element.Destroy ();
            Element = null;
        }

        #endregion
    }
}