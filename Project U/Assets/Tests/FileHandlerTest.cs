using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Project_U.Assets.Scripts;
using System.IO;

public class FileHandlerTest {

    class NineNotSavable: Saveable
    {
        public string GetSaveLocation()
        {
             return "ni/ne";
        }
    }
    
    class NineSavable: Saveable
    {
        public string Yay = "woohoo";
        public string GetSaveLocation()
        {
             return Path.Combine("Tests", "FileHandlerTest.cs");
        }
    }
    
    [Test]
    public void Save_PathInvalid() 
    {
        NineNotSavable nine = new NineNotSavable();
        Assert.Throws<System.IO.DirectoryNotFoundException>(() => FileHandler.Save(nine, "nine"),"fail");
    }
    
    [Test]
    public void Load_PathInvalid()
    {
        NineSavable nine = new NineSavable();
        var newNine = FileHandler.Load<NineSavable>("t.json");
    }
}
