using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Here we will hava all the information about the world, type, textures, behaviour...
//Basically this controlls the behaviour of external
namespace Core.Enviroment {
    internal class EnviromentController {
        internal EnviromentController() {
            Load();
        }

        internal WorldBehaviour world = new WorldBehaviour("Original");

        internal void Load() {
            Console.WriteLine("Base for game files : " + YamlImporter.BaseLocation);
            
        }


    }
}
