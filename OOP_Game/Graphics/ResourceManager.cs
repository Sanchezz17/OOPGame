using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace OOP_Game
{
    public class ResourceManager
    {
        public readonly Dictionary<string, VisualObject> VisualObjects = new Dictionary<string, VisualObject>();

        public ResourceManager()
        {
            var heroesDir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Heroes");
            foreach (var heroDir in heroesDir.EnumerateDirectories())
            {
                var visualObject = new VisualObject();
                VisualObjects[heroDir.Name] = visualObject;
                visualObject.PassiveImage = Image.FromFile(heroDir.FullName + @"\static.gif");
                visualObject.AttackImage = Image.FromFile(heroDir.FullName + @"\attack.gif");
            }
            
            var malefactorsDir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Malefactors");
            foreach (var malefactorDir in malefactorsDir.EnumerateDirectories())
            {
                var visualObject = new VisualObject();
                VisualObjects[malefactorDir.Name] = visualObject;
                visualObject.PassiveImage = Image.FromFile(malefactorDir.FullName + @"\static.gif");
                visualObject.AttackImage = Image.FromFile(malefactorDir.FullName + @"\attack.gif");
                //visualObject.MoveImage = Image.FromFile(malefactorDir.FullName + @"\move.gif");
            }
            //var gemVisual = new VisualObject();
            //VisualObjects[]
        }
        
    }
}