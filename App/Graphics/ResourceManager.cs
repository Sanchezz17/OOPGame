using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace App.Graphics
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
                visualObject.PassiveImage = new Bitmap(heroDir.FullName + @"\passive.gif");
                visualObject.AttackImage = new Bitmap(heroDir.FullName + @"\attack.gif");
            }
            
            var malefactorsDir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Malefactors");
            foreach (var malefactorDir in malefactorsDir.EnumerateDirectories())
            {
                var visualObject = new VisualObject();
                VisualObjects[malefactorDir.Name] = visualObject;
                visualObject.PassiveImage = new Bitmap(malefactorDir.FullName + @"\passive.gif");
                visualObject.AttackImage = new Bitmap(malefactorDir.FullName + @"\attack.gif");
                visualObject.MoveImage = new Bitmap(malefactorDir.FullName + @"\move.gif");
            }
            
            var shotsDir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Strikes");
            foreach (var shotDir in shotsDir.EnumerateDirectories())
            {
                var visualObject = new VisualObject();
                VisualObjects[shotDir.Name] = visualObject;
                visualObject.PassiveImage = new Bitmap(
                    shotDir.FullName + $@"\{shotDir.Name}.gif");
                visualObject.MoveImage = visualObject.PassiveImage;
            }
            
            var gemVisual = new VisualObject();
            VisualObjects["Gem"] = gemVisual;
            gemVisual.PassiveImage = new Bitmap(Environment.CurrentDirectory + @"\Resources\gem.gif");
            gemVisual.MoveImage = gemVisual.PassiveImage;  
            
            var coinsVisual = new VisualObject();
            VisualObjects["Coins"] = coinsVisual;
            coinsVisual.PassiveImage = new Bitmap(Environment.CurrentDirectory + @"\Resources\coins.png");
            coinsVisual.MoveImage = coinsVisual.PassiveImage;
        }
    }
}