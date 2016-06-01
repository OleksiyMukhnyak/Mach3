using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Xml;

public class PostImporting : AssetPostprocessor
{
    static bool imported = false;
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {


        if (Directory.Exists("Assets/Plugins/Android/google-play-services_lib") && Directory.Exists("Assets/PlayServicesResolver"))
        {
            bool check = AssetDatabase.DeleteAsset("Assets/Plugins/Android/google-play-services_lib");
            if (check)
                Debug.Log("deleted google-play-services_lib ");
        }

        if (Directory.Exists("Assets/FacebookSDK"))
        {
            ModifyManifest();
        }

        if (Directory.Exists("Assets/PlayServicesResolver"))
        {
            //if (!imported)
            //{

            //    AssetDatabase.ImportAsset("Assets/PlayServicesResolver");
            //Debug.Log("assets reimorted");
            //}
        }
        //if (!EditorPrefs.GetBool("notfirsttime"))
        //{
        //    EditorApplication.OpenScene("Assets/JellyGarden/Scenes/game.unity");
        //    EditorApplication.ExecuteMenuItem("Window/Jelly Garder editor");
        //    if (AssetDatabase.IsValidFolder("Assets/JellyGarden/Facebook"))
        //    {
        //        AssetDatabase.DeleteAsset("Assets/JellyGarden/Facebook");
        //        AssetDatabase.DeleteAsset("Assets/Plugins/Android/facebook");
        //    }
        //    AssetDatabase.MoveAsset("Assets/JellyGarden/Plugins", "Assets/Plugins");
        //    AssetDatabase.MoveAsset("Assets/JellyGarden/FacebookSDK", "Assets/FacebookSDK");
        //    AssetDatabase.ImportAsset("Assets/Plugins");
        //    Debug.ClearDeveloperConsole();
        //    EditorPrefs.SetBool("notfirsttime", true);
        //}
    }

    //    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    //{
    //    Debug.Log("Sprites: " + sprites.Length);
    //}

    static void ModifyManifest()
    {
        var outputFile = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
        if (File.Exists(outputFile))
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(outputFile);

            if (doc == null)
            {
                //Debug.LogError("Couldn't load " + outputFile);
                return;
            }
            XmlNode manNode = FindChildNode(doc, "manifest");
            XmlNode dict = FindChildNode(manNode, "uses-sdk");
            if (dict == null)
            {
                //  Debug.LogError("Error parsing " + outputFile);
                return;
            }

            XmlAttributeCollection attributes = dict.Attributes;
            XmlAttribute attr = attributes[0];
            if (attr.Name == "android:minSdkVersion")
            {
                attr.Value = "" + 15;
            }

            doc.Save(outputFile);

            //while (curr != null)
            //{
            //    var currXmlElement = curr as XmlElement;
            //    Debug.Log(curr.Name);
            //    curr = curr.NextSibling;
            //}
        }
    }

    private static XmlNode FindChildNode(XmlNode parent, string name)
    {
        XmlNode curr = parent.FirstChild;
        while (curr != null)
        {
            if (curr.Name.Equals(name))
            {
                return curr;
            }

            curr = curr.NextSibling;
        }

        return null;
    }

    //private static void SetOrReplaceXmlElement(
    //XmlNode parent,
    //XmlElement newElement)
    //{
    //    string attrNameValue = newElement.GetAttribute("name");
    //    string elementType = newElement.Name;

    //    XmlElement existingElment;
    //    if (TryFindElementWithAndroidName(parent, attrNameValue, out existingElment, elementType))
    //    {
    //        parent.ReplaceChild(newElement, existingElment);
    //    }
    //    else
    //    {
    //        parent.AppendChild(newElement);
    //    }
    //}

}