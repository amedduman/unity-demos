using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    public static class Game
    {

        #region Data

        public static GridData gridData { get; private set; } = new GridData();
        public static WordInputData wordInputData = new WordInputData();
        public static List<WordInputData> WordInputDataList = new List<WordInputData>();
        public static List<string> words = new List<string>();

        #endregion

        #region Events

        public static OnGridCreated onGridCreated { get; private set; } = new OnGridCreated();
        public static OnGenerateBtnClicked onGenerateBtnClicked { get; private set; } = new OnGenerateBtnClicked();
        public static OnStartingTileAndDirectionSet onStartingTileAndDirectionSet { get; private set; } =
            new OnStartingTileAndDirectionSet();
        public static OnWordCreationPanelEnterButtonPressed
            onWordCreationPanelEnterButtonPressed { get; private set; } = new OnWordCreationPanelEnterButtonPressed();

        #endregion
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void ResetData()
        {
        }
    }

    #region Data

    public class GridData
    {
        public GridData()
        {
            Debug.Log("grid data created");
        }
        public int rows { get; private set; }
        public int columns { get; private set; }
        List<Tile> myTiles  = new List<Tile>();
        public IReadOnlyList<Tile> tiles => myTiles.AsReadOnly();

        // since this is a scriptable object we need to reset its fields
        public void ResetData()
        {
            myTiles.Clear();
            rows = 0;
            columns = 0;
        }
        
        public void SetTileData(List<Tile> in_tiles, int in_rows, int in_columns)
        {
            myTiles = in_tiles;
            rows = in_rows;
            columns = in_columns;
        }
    }
    
    public struct WordInputData
    {
        public Tile tile;
        public WordCreationDirectionE dir;
        public string word;
    }

    #endregion

    #region Events

    public class OnGridCreated : MyEvent<OnGridCreated.Data>
    {
        public struct Data
        {
            public Vector3 center;
            public Bounds bounds;
            public Bounds boundsBeforeBuffer;
            
            public Data(Vector3 center, Bounds bounds, Bounds boundsBeforeBuffer)
            {
                this.center = center;
                this.bounds = bounds;
                this.boundsBeforeBuffer = boundsBeforeBuffer;
            }
        }
    }
    
    public class OnGenerateBtnClicked : MyEvent<OnGenerateBtnClicked.Data>
    {
        public void CallTheEvent(int rows, int columns)
        {
            Invoke(new Data(rows, columns));
        }
        
        public struct Data
        {
            public int rows { get; private set; }
            public int columns { get; private set; }

            public Data(int rows, int columns)
            {
                this.rows = rows;
                this.columns = columns;
            }
        }
    }

    public class OnStartingTileAndDirectionSet : MyEventParameterless { }

    public class OnWordCreationPanelEnterButtonPressed : MyEventParameterless {}

    #endregion
    
    public enum WordCreationDirectionE
    {
        none = 0,
        right = 10,
        down = 20,
    }
}
