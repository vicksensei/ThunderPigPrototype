using System.IO;
using UnityEngine;
using UnityEditor;
using System;
namespace SOEvents
{
    /**/
    public class UnityEditorWindow : EditorWindow
    {

        string sName, sType;
        [MenuItem("Window/Event Type")]
        public static void ShowWindow()
        {
            GetWindow<UnityEditorWindow>("Event Type");
        }


        private void OnGUI()
        {
            GUILayout.Label("Create an Event for a new Variable", EditorStyles.boldLabel);
            sName = EditorGUILayout.TextField("Name", sName);
            GUILayout.Label("Name must be all leters, \n" +
                "no spaces or special characters");
            sType = EditorGUILayout.TextField("Variable type", sType);
            GUILayout.Label("Event type must be all leters,\n" +
                "no spaces or special characters, \n" +
                "must match existing data type or class\n" +
                "and is case sensitive");
            if (GUILayout.Button("Create"))
            {
                Debug.Log("Create file of name " + sName + "  and type " + sType);
                CreateFilesFromTemplate();
            }
        }

        private void CreateFilesFromTemplate()
        {
            if (CheckString(sName) && CheckString(sType))
            {
                sName = sName.ToLower();
                if (CapitalizeFirstLetter(sName) != null)
                {
                    sName = CapitalizeFirstLetter(sName);
                }

                WriteCS("Events", "TemplateEvent", sName + "Event");
                WriteCS("Listeners", "TemplateListener", sName + "Listener");
                WriteCS("UnityEvents", "UnityTemplateEvent", "Unity" + sName + "Event");
                AssetDatabase.Refresh();

            }
            else
            {
                Debug.Log("Invalid Name or Type");
            }
        }

        private bool CheckString(string sToTest)
        {
            char[] charsToTrim = { ' ', '.' };
            sToTest = sToTest.Trim(charsToTrim);
            if (sToTest == "") { return false; }
            if (sToTest.Contains("/")) { return false; }
            if (sToTest.Contains(" ")) { return false; }
            if (sToTest.Contains("{")) { return false; }
            if (sToTest.Contains("}")) { return false; }
            if (sToTest.Contains("[")) { return false; }
            if (sToTest.Contains(")")) { return false; }
            if (sToTest.Contains("(")) { return false; }
            if (sToTest.Contains("]")) { return false; }
            return true;
        }

        string CapitalizeFirstLetter(string str)
        {
            string output;
            if (str.Length == 0)
            {
                Debug.Log("Empty String");
                return null;
            }
            else if (str.Length == 1)
                output = char.ToUpper(str[0]) + "";
            else
                output = (char.ToUpper(str[0]) + str.Substring(1));
            return output;
        }

        string ReadTemplate(string template)
        {

            string filePath, fileContent;

            filePath = Application.dataPath + "/EventSystem/Templates/" + template + ".txt";

            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(filePath))
                {
                    // Read the stream as a string, and write the string to the console.
                    fileContent = sr.ReadToEnd();
                    return fileContent;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return null;
        }
        void WriteCS(string folder, string templateName, string outputName)
        {
            string file, path;
            file = ReadTemplate(templateName);
            if (file != null)
            {
                file = file.Replace("[Name]", sName);
                file = file.Replace("[type]", sType);
                path = Application.dataPath + "/EventSystem/" + folder + "/" + outputName + ".cs";
                if (!File.Exists(path))
                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.Write(file);
                    }
                else
                {
                    Debug.Log("Event Type already exists!");
                }
            }
            else
            {
                Debug.Log("File" + outputName + "could not be written");
            }
        }
    }
}
