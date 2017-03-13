using System.Collections.Generic;

namespace Match3Test
{
    public class TypeEqualMatchingStrategy : BaseMathchingStategy
    {
        #region Private Methods

        private List<ElementController> _horizontalLine;
        private List<ElementController> _verticalLine;
        private FieldController _field;

        #endregion

        #region Public Methods

        public override bool TryMatch (FieldController field, TileController firstTile, TileController secondTile)
        {
            _field = field;

            //Loop through each element on the field
            for (var t = 0; t < _field.Rows.Count; t++)
            {
                for (var k = 0; k < _field.Rows [t].Tiles.Count; k++) 
                {
                    if (_field.Rows [t].Tiles [k].IsEmpty)
                        continue;
                    
                    var currentElement = _field.Rows [t].Tiles [k].Element;
                    _horizontalLine = new List<ElementController> ();
                    _verticalLine = new List<ElementController> ();

                    GetHorizontalMatch (currentElement, t, k);
                    GetVerticalMatch (currentElement, t, k);

                    if (_horizontalLine.Count > 1) 
                    {
                        currentElement.MarkedToDestroy = true;
                        foreach (var element in _horizontalLine)
                            element.MarkedToDestroy = true;
                        
                    }

                    if (_verticalLine.Count > 1) 
                    {
                        currentElement.MarkedToDestroy = true;
                        foreach (var element in _verticalLine)
                            element.MarkedToDestroy = true;
                    }
                }
            }

            bool retVal = false;

            foreach (var row in _field.Rows) 
            {
                foreach (var tile in row.Tiles) 
                {
                    if (!tile.IsEmpty && tile.Element.MarkedToDestroy)
                    {
                        retVal = true;
                        tile.Element.Destroy ();
                        tile.Element = null;
                    }
                }
            }
            return retVal;
        }

        #endregion

        #region Private Methods

        private bool ElementsMatch (ElementController element, TileController tile2)
        {
            if (tile2.IsEmpty)
                return false;
            if (element.Model.MatchingTypes.Contains (tile2.Element.Model.Type))
                return true;
            return false;
        }


        private void GetHorizontalMatch (ElementController current, int row, int tile)
        {
            //go right neighbours
            if ((row + 1) < _field.Rows.Count - 1) 
            {
                for (var i = row + 1; i < _field.Rows.Count; i++) 
                {
                    //Matching neighbour found
                    if (ElementsMatch (current, _field.Rows [i].Tiles [tile]))
                    {
                        var element = _field.Rows [i].Tiles [tile].Element;
                        _horizontalLine.Add (element);
                    }
                    //Neigbour is missing or not matching
                    else  { break; }
                }
            }

            //go left neighbours
            if ((row - 1) >= 0) 
            {
                for (var i = row - 1; i >= 0; i--) 
                {
                    //Matching neighbour found
                    if (ElementsMatch (current, _field.Rows[i].Tiles[tile])) 
                    {
                        var element = _field.Rows [i].Tiles [tile].Element;
                        _horizontalLine.Add (element);
                    }
                    //Neigbour is missing or not matching
                    else { break; }
                }
            }
        }

        private void GetVerticalMatch (ElementController current, int row, int tile)
        {
            //go above neighbours
            if ((tile + 1) < _field.Rows[row].Tiles.Count - 1) 
            {
                for (var i = tile + 1; i < _field.Rows [row].Tiles.Count; i++) 
                {
                    //Matching neighbour found
                    if (ElementsMatch (current, _field.Rows [row].Tiles [i])) {
                        var element = _field.Rows [row].Tiles [i].Element;
                        _verticalLine.Add (element);
                    }
                    //Neigbour is missing or not matching
                    else { break; }
                }
            }

            //go below neighbours
            if ((tile - 1) >= 0 ) 
            {
                for (var i = tile - 1; i >= 0; i--) 
                {
                    //Matching neighbour found
                    if (ElementsMatch (current, _field.Rows [row].Tiles [i])) {
                        var element = _field.Rows [row].Tiles [i].Element;
                        _verticalLine.Add (element);
                    }
                    //Neigbour is missing or not matching
                    else { break; }
                }
            }
        }

        #endregion
    }
}