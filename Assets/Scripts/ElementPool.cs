using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Match3Test
{
    public class ElementPool : MonoBehaviour
    {
        #region Public Properties

        public Transform PoolTransform 
        {
            get 
            {
                if (_poolTransform == null)
                    _poolTransform = transform;
                return _poolTransform;
            }
        }

        #endregion

        #region Private Variables

        private LevelModel _levelModel;
        private Transform _poolTransform;
        [SerializeField]
        private List<ElementController> _elements = new List<ElementController> ();
        private GameObjectFactory _factory = new GameObjectFactory ();
        private System.Random _random = new System.Random ();
        private int _generatableModelsAmount;

        #endregion

        #region MonoBahaviour Methods

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        private void OnDestroy ()
        {
            foreach (var element in _elements)
                element.Destroyed -= OnElementDestroyed;
        }

        #endregion

        #region Public Methods

        public void Initialize (LevelModel levelModel)
        {
            _levelModel = levelModel;
            _generatableModelsAmount = _levelModel.ElementModels.Count (x => x.Generatable) - 1;
        }

        public ElementController GetElement (bool randomOne = true, ElementType type = ElementType.SugarCookie)
        {
            ElementController retVal;
            if (_elements.Count == 0)
            {
                retVal = _factory.CreatedGameObject<ElementController> (_levelModel.ElementPrefab, PoolTransform);
                retVal.Destroyed += OnElementDestroyed;
            }
            else
            {
                retVal = _elements.FirstOrDefault ();
                _elements.Remove (retVal);
            }

            ElementTemplateModel template;
            if (randomOne) 
            {
                var rndIndex = _random.Next (0, _generatableModelsAmount);
                template = _levelModel.ElementModels[rndIndex];
            }
            else { template = GetElementTemplateModelByType (type); }

            retVal.Model = GetElementModelByTempalte (template);
            retVal.GO.SetActive (true);

            return retVal;
        }

        public void OnElementDestroyed (ElementController element)
        {
            element.GO.SetActive (false);
            element.MarkedToDestroy = false;
            element.IsSelected = false;
            element.ElementTransform.parent = PoolTransform;
            _elements.Add (element);
        }

        #endregion


        #region Private Methods

        private ElementTemplateModel GetElementTemplateModelByType (ElementType type)
        {
            return _levelModel.ElementModels.FirstOrDefault (x => x.Type == type);
        }

        private BaseElementModel GetElementModelByTempalte (ElementTemplateModel template)
        {
            if (template.Type == ElementType.SmallBomb) 
                return new SmallBombElementModel (template.Type, template.NormalSprite, template.SelectedSprite);

            return new ColoredElementModel (template.Type, template.NormalSprite, template.SelectedSprite);
        }

        #endregion
    }
}