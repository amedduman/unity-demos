using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WordGame
{
    public static class LevelDataSaver
    {
        public const string dimensionIndicator = "[dim]";
        public const string emptyTileIndicator = "[emp]";
        public const string fullTileIndicator = "[full]";
        public const string wordIndicator = "[word]";
        public static void Save(string fileName)
        {
            // Specify the path to your text file
            string relativeFilePath = "_game/texts/" + fileName + ".txt";
            string filePath = Path.Combine(Application.dataPath, relativeFilePath);

            // Use StreamWriter within a using block to ensure proper resource disposal
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(dimensionIndicator + " " +Game.gridData.columns + " " + Game.gridData.rows);
                writer.Write(writer.NewLine);

                foreach (Tile tile in Game.gridData.tiles)
                {
                    var prefix = string.Empty;
                    if (string.IsNullOrEmpty(tile.GetLetter()))
                        prefix = emptyTileIndicator;
                    else
                        prefix = fullTileIndicator;
                    writer.Write( prefix + " " + tile.cellPos.x + " " + tile.cellPos.y + " " + tile.GetLetter());
                    writer.Write(writer.NewLine);
                }
                
                foreach (var wordInputData in Game.WordInputDataList)
                {
                    writer.Write($"{wordIndicator} {wordInputData.word} {wordInputData.tile.cellPos.x},{wordInputData.tile.cellPos.y} {wordInputData.dir}");
                    // writer.Write(wordIndicator + " " + word);
                    writer.Write(writer.NewLine);
                }
            }

            Debug.Log("File written successfully at: " + filePath);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}