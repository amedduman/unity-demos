using System.IO;
using UnityEngine;

namespace WordGame
{
    public static class LevelDataSaver
    {
        public static void Save(string fileName)
        {
            // Specify the path to your text file
            string relativeFilePath = "_game/texts/" + fileName + ".txt";
            string filePath = Path.Combine(Application.dataPath, relativeFilePath);

            // Example content to write to the file
            string contentToWrite = "Hello, this is some text to write to the file.";

            // Use StreamWriter within a using block to ensure proper resource disposal
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the content to the file
                writer.Write(contentToWrite);
            }

            Debug.Log("File written successfully at: " + filePath);
        }
    }
}