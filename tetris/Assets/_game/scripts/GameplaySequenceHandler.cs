using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class GameplaySequenceHandler
    {
        public static GameplaySequenceHandler instance;
        readonly GridHandler gridHandler;

        public GameplaySequenceHandler(GridHandler gridHandler)
        {
            instance = this;
            this.gridHandler = gridHandler;
        }
        
        public static void OnBlockLaunched(List<Vector3> unitPositions)
        {
            instance.gridHandler.SetTilesOccupied();
        }
    }
}