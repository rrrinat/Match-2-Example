using UnityEngine;
using UnityEditor;
using System.IO;

namespace AkilGames
{
    public class CodeCounter : EditorWindow
    {
        [MenuItem("AkilGames/Code counter")]
        public static void ShowWindow()
        {
            Calculate();
            GetWindow(typeof(CodeCounter), false, "Code counter");
        }

        static int importedCountCs;
        static int importedCountXml;

        static int akilGamesCountCs;
        static int akilGamesCountXml;

        static int projCountCs;
        static int projCountXml;

        static void Calculate()
        {
            string gamePath = Application.dataPath;
            importedCountCs = CountLinesInFolder(gamePath, "*.cs");
            importedCountXml = CountLinesInFolder(gamePath, "*.xml");

            if (Directory.Exists(gamePath + "/Standard Assets/AkilGames/"))
            {
                akilGamesCountCs = CountLinesInFolder(gamePath + "/Standard Assets/AkilGames/", "*.cs");
                akilGamesCountXml = CountLinesInFolder(gamePath + "/Standard Assets/AkilGames/", "*.xml");
            }
            if (Directory.Exists(gamePath + "/Scripts/"))
            {
                projCountCs = CountLinesInFolder(gamePath + "/Scripts/", "*.cs");
                projCountXml = CountLinesInFolder(gamePath + "/Scripts/", "*.xml");
            }
            if (Directory.Exists(gamePath + "/Resources/"))
            {
                projCountXml += CountLinesInFolder(gamePath + "/Resources/", "*.xml");
            }

            importedCountCs -= projCountCs;
            importedCountXml -= projCountXml;

            importedCountCs -= akilGamesCountCs;
            importedCountXml -= akilGamesCountXml;
        }

        static int CountLinesInFolder(string path, string filter = "*.cs")
        {
            int count = 0;
            foreach (var file in Directory.GetFiles(path, filter))
            {
                count += File.ReadAllLines(file).Length;
            }
            foreach (var dirrectory in Directory.GetDirectories(path))
            {
                count += CountLinesInFolder(dirrectory, filter);
            }
            return count;
        }

        void OnGUI()
        {
            using (var verticalScope = new GUILayout.VerticalScope(GUILayout.Width(200), GUILayout.Height(95)))
            {
                GUILayout.Label("Imported code lines:", EditorStyles.boldLabel, GUILayout.Height(20));
                GUILayout.Label($"C# code: {importedCountCs}");
                GUILayout.Label($"Xml text: {importedCountXml}");

                GUILayout.Space(5.0f);
                GUILayout.Label("Project code lines:", EditorStyles.boldLabel, GUILayout.Height(20));
                GUILayout.Label($"C# code: {projCountCs}");
                GUILayout.Label($"Xml text: {projCountXml}");

                GUILayout.Space(5.0f);
                GUILayout.Label("AkilGames code lines:", EditorStyles.boldLabel, GUILayout.Height(20));
                GUILayout.Label($"C# code: {akilGamesCountCs}");
                GUILayout.Label($"Xml text: {akilGamesCountXml}");

                GUILayout.Space(5.0f);
                GUILayout.Label(string.Format("Total C# code lines: {0}", importedCountCs + projCountCs + akilGamesCountCs), EditorStyles.boldLabel, GUILayout.Height(20));
                GUILayout.Label(string.Format("Total Xml text lines: {0}", importedCountXml + projCountXml + akilGamesCountXml), EditorStyles.boldLabel, GUILayout.Height(20));

                GUILayout.Space(5.0f);
                if (GUILayout.Button("Recalculate", GUILayout.Height(20)))
                {
                    Calculate();
                }
            }
        }
    }
}