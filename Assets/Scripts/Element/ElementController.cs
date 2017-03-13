using UnityEngine;
using System.Collections;

namespace Match3Test
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ElementController : MonoBehaviour
    {
        #region Inspector Set Varibles

        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private SpriteRenderer _renderer;

        #endregion

        #region Public Properties

        /// <summary>
        /// Model is a set/get property because of pool usage
        /// </summary>
        /// <value>The model.</value>
        public BaseElementModel Model
        {
            get { return _model; }
            set 
            {
                _model = value;
                _renderer.color = Color.white;
                _renderer.sprite = _model.NormalSprite;
            }
        }

        public bool IsSelected 
        {
            get { return _isSelected; }
            set 
            {
                _isSelected = value;
                _renderer.sprite = _isSelected ?
                                    _model.SelectedSprite :
                                    _model.NormalSprite;
            }
        }

        public Transform ElementTransform 
        {
            get 
            {
                if (_tranform == null)
                _tranform = transform;
                return _tranform;
            }
        }

        public GameObject GO 
        {
            get 
            {
                if (_gameobject == null)
                    _gameobject = gameObject;
                return _gameobject;
            }
        }

        public bool IsAnimating { get { return _isAnimating; } }

        public bool MarkedToDestroy { get; set; }

        #endregion

        #region Public Events

        public ElementDestroyed Destroyed;

        #endregion

        #region Private Varbles

        [SerializeField]
        private BaseElementModel _model;
        private bool _isSelected;
        private Vector3 _helperPosition;
        private Transform _tranform;
        private GameObject _gameobject;
        private bool _isAnimating;

        #endregion

        #region MonoBehaviour Methods

        private void Start ()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer> ();
        }

        #endregion

        #region Public Methods

        public IEnumerator CenterInNewParent()
        {
            _isAnimating = true;
            while (Mathf.Abs( Vector2.Distance (ElementTransform.localPosition, Vector2.zero)) > 0.01f) 
            {
                var currPost = Vector2.Lerp (ElementTransform.localPosition, Vector2.zero, Time.deltaTime * _moveSpeed);
                ElementTransform.localPosition = currPost;
                yield return null;
            }
            ElementTransform.localPosition = Vector2.zero;
            _isAnimating = false;
        }


        public void Center ()
        {
            ElementTransform.localPosition = Vector2.zero;
        }

        public void Destroy ()
        {
            StartCoroutine (DestroyAnimation ());
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// Alpha fading destroy animation
        /// </summary>
        /// <returns>The animation.</returns>
        private IEnumerator DestroyAnimation ()
        {
            var spriteColor = _renderer.color;
            while (spriteColor.a > 0) 
            {
                spriteColor.a -= 0.1f;
                _renderer.color = spriteColor;
                yield return null;
            }
            if (Destroyed != null)
                Destroyed (this);
        }

        #endregion
    }
}