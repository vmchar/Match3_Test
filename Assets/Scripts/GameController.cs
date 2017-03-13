using UnityEngine;
using System.Collections;
using System.Linq;

namespace Match3Test
{
    /// <summary>
    /// Prototype is build with Unity3d v5.4.4f1 and Xamarin on MacOs.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region Inspector Set Varables

        [SerializeField]
        private FieldModel _fieldModel;

        [SerializeField]
        private LevelModel _levelModel;

        [SerializeField]
        private GameObject _fieldGameobject;

        [SerializeField]
        private ElementPool _pool;

        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private LayerMask _tileLayers;

        #endregion

        #region Private Variables

        private FieldController _field;
        private bool _levelIsFilling = false;
        private readonly BaseMathchingStategy _standartMatchingStategy = new TypeEqualMatchingStrategy ();
        private BaseMathchingStategy _currentMatchingStrategy;
        private TileController _firstTile;
        private TileController _secondTile;
        private RaycastHit _hit;

        #endregion

        #region MonoBehaviour Methods

        private void Start ()
        {
            _field = new FieldBuilder().BuildField(_fieldGameobject, _fieldModel);
            _pool.Initialize (_levelModel);

            if (_mainCamera == null)
                _mainCamera = Camera.main;

            StartCoroutine (FillInTheLevel ());

            _currentMatchingStrategy = _standartMatchingStategy;
        }

        private void Update () 
        {
            //Wait until level filling ends
            if (_levelIsFilling)
                return;

            //Wait until switching animation ends
            if (_firstTile != null && _secondTile != null) 
            {
                if (_firstTile.Element.IsAnimating || _secondTile.Element.IsAnimating)
                    return;
            }

            //Perform matching
            if (_currentMatchingStrategy != null) 
            {
                if (TryMatching (_currentMatchingStrategy)) 
                {
                    //Forget previous move
                    _firstTile = null;
                    _secondTile = null;
                    StartCoroutine (FillInTheLevel ());
                    return;
                }
                else
                {
                    //Undo last move
                    if (_firstTile != null && _secondTile != null) 
                    { SwitchElements (_firstTile, _secondTile); }
                    _currentMatchingStrategy = null;
                    return;
                }
            }

            // if there is no animations get user interaction
            if (Input.GetMouseButtonDown (0)) 
            {
                var mousePosition = Input.mousePosition;
                mousePosition = _mainCamera.ScreenToWorldPoint (mousePosition);
                mousePosition.z = -1.0f;

                Physics.Raycast (mousePosition, new Vector3 (0, 0, 1), out _hit, _tileLayers);
                if (_hit.transform != null) 
                {
                    var hitTile = _hit.transform.GetComponent<TileController> ();
                    if (hitTile != null) 
                    {
                        if (_firstTile == null) 
                        {
                            _firstTile = hitTile;
                            _firstTile.Element.IsSelected = true;
                        }
                        else 
                        {
                            _secondTile = hitTile;
                            _secondTile.Element.IsSelected = true;

                            //Deselect item
                            if (_secondTile == _firstTile) 
                            {
                                _secondTile = null;
                                _firstTile.Element.IsSelected = false;
                                _firstTile = null;
                            }
                        }

                        //Switch pair selected - perform switch
                        if (_firstTile != null && _secondTile != null) 
                        {
                            _firstTile.Element.IsSelected = false;
                            _secondTile.Element.IsSelected = false;
                            SwitchElements (_firstTile, _secondTile);

                            _currentMatchingStrategy = _firstTile.Element.Model.Strategy;
                            if (_currentMatchingStrategy == null) 
                            {
                                _currentMatchingStrategy = _standartMatchingStategy;
                            }
                        }
                    }
                }
            }
	    }

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        private void OnDestroy ()
        {
            foreach (var row in _field.Rows) 
            {
                foreach (var tile in row.Tiles) 
                {
                    if (tile.IsEmpty)
                        continue;
                    tile.Element.Destroyed -= _pool.OnElementDestroyed;
                }
            }
        }

        #endregion

        #region Private Methods

        private bool TryMatching (BaseMathchingStategy strategy = null)
        {
            if (strategy == null)
                strategy = _standartMatchingStategy;

            return strategy.TryMatch (_field, null, null);
        }

        private IEnumerator FillInTheLevel ()
        {
            _levelIsFilling = true;

            while (_field.Rows.Count (x => !x.IsFull) > 0)
            {
                //Check for missing elements on the level
                for (var i = 0; i < _field.Rows.Count; i++) 
                {
                    var currentRow = _field.Rows [i];
                    for (var j = 1; j < currentRow.Tiles.Count; j++)
                    {

                        //Search for fist no-null element with previous null element
                        if (currentRow.Tiles [j].IsEmpty == false && currentRow.Tiles [j - 1].IsEmpty == true)
                        {
                            var element = currentRow.Tiles [j].Element;
                            currentRow.Tiles [j].Element = null;
                            element.ElementTransform.parent = currentRow.Tiles [j - 1].TileTransform;
                            currentRow.Tiles [j - 1].Element = element;
                            StartCoroutine (element.CenterInNewParent ());
                            //break;
                        }
                         
                        else if (j == currentRow.Tiles.Count - 1)
                        {
                            //Last element in row if emty -> need to create a new one
                            if (currentRow.Tiles [j].IsEmpty) 
                            {
                                var element = CreateEmptyInRow (i);
                                StartCoroutine (element.CenterInNewParent ());
                            } 
                        }
                    }
                }

                yield return new WaitForSeconds (0.1f);
            }

            _levelIsFilling = false;
        }

        private ElementController CreateEmptyInRow (int rowIndex)
        {
            var tile = _field.Rows [rowIndex].Tiles [_field.Rows [rowIndex].Tiles.Count - 1];
            var element = _pool.GetElement ();
            element.ElementTransform.position = _field.Rows [rowIndex].ElementEmitterTransform.position;
            tile.Element = element;
            element.ElementTransform.parent = tile.TileTransform;
            return element;
        }


        private void SwitchElements (TileController tile1, TileController tile2)
        {
            var element = tile1.Element;
            tile1.Element = tile2.Element;
            tile1.Element.ElementTransform.parent = tile1.TileTransform;
            tile1.Element.Center ();

            tile2.Element = element;
            tile2.Element.ElementTransform.parent = tile2.TileTransform;
            tile2.Element.Center ();
        }


        #endregion

    }   
}