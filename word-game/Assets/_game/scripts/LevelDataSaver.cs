using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WordGame
{
    public static class LevelDataSaver
    {
        public static void Save(string fileName)
        {
            // Specify the path to your text file
            string relativeFilePath = "_game/texts/" + fileName + ".txt";
            string filePath = Path.Combine(Application.dataPath, relativeFilePath);

            // Use StreamWriter within a using block to ensure proper resource disposal
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(Game.gridData.columns + " " + Game.gridData.rows);
                writer.Write(writer.NewLine);

                foreach (Tile tile in Game.gridData.tiles)
                {
                    writer.Write(tile.cellPos.x + " " + tile.cellPos.y + " " + tile.GetLetter());
                    writer.Write(writer.NewLine);
                }
                
                foreach (var word in Game.words)
                {
                    writer.Write(word);
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