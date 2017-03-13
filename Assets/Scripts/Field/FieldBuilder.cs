using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Match3Test
{
    public class FieldBuilder
    {
        #region Private Variables

        private FieldModel _model;
        private GameObject _fieldObject;
        private GameObjectFactory _factory = new GameObjectFactory ();

        #endregion

        #region Public Methods

        public FieldController BuildField (GameObject field, FieldModel model)
        {
            _model = model;
            _fieldObject = field;

            var fieldController = _fieldObject.AddComponent<FieldController> ();
            fieldController.Initialize (
                                       new List<RowController> (CreateRowsFofField (
                                                                               fieldController.FieldTransform,
                                                                               _model.RowPrefab,
                                                                               _model.Xdimention)
                                                               )
                                       );
            return fieldController;
        }

        #endregion

        #region Private Methods

        private IEnumerable<RowController> CreateRowsFofField ( Transform parent, GameObject rowPrefab, int dimention)
        {
            //Calculate first row x position
            var lastRowPos = ( (dimention * _model.TileXSize) )/ 2;
            var currentRowXposition = -(lastRowPos) + (_model.TileXSize / 2) ;

            for (var i = 0; i < dimention; i++) 
            {
                //Instantiate row
                var row = _factory.CreatedGameObject<RowController> (_model.RowPrefab, parent);
                //Calculate right position for the row to center in vertically and set proper x coordinate
                var rowPos = new Vector2 (currentRowXposition, 0);

                currentRowXposition += _model.TileXSize;
                row.RowTransform.localPosition = rowPos;
                var rowTiles = new List<TileController> (CreateTilesForRow (row.RowTransform,
                                                                            _model.TilePrefab,
                                                                            _model.Ydimention)
                                                        );
                //Create emitter for the row
                var emitterGO = GameObject.Instantiate (_model.ElementEmitterPrefab);
                var topMostElementPosion = rowTiles.LastOrDefault ().TileTransform.localPosition;
                topMostElementPosion.y += _model.TileYSize;
                var emitterTransform = emitterGO.transform;
                emitterTransform.parent = row.RowTransform;
                emitterTransform.localPosition = topMostElementPosion;

                row.Initialize (_model.Ydimention, rowTiles, emitterTransform);
                yield return row;
            }
        }

        private IEnumerable<TileController> CreateTilesForRow (Transform parent, GameObject tilePrefab, int rowDimention)
        {
            var currentTilePosition = (rowDimention & 2) == 0 ?
                                       new Vector2 (0, _model.TileYSize) :
                                       Vector2.zero;

            //+1 constant here is for the emmiter which is not a tile but is the same size
            var lastRowPos = (((rowDimention+1) * _model.TileYSize)) / 2;
            var currentRowXposition = -(lastRowPos) + (_model.TileYSize / 2);
            currentTilePosition.y = currentRowXposition;
            
            for (var i = 0; i < rowDimention; i++) 
            {
                var tile = _factory.CreatedGameObject<TileController> (tilePrefab, parent);
                tile.TileTransform.localPosition = currentTilePosition;
                currentTilePosition.y += _model.TileYSize;
                yield return tile;
            }
        }
 
        #endregion
    }
}