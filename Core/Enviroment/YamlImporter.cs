using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpYaml;
using SharpYaml.Serialization;

namespace Core.Enviroment {

    static class YamlImporter {
        internal static string BaseLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            + "/Blocks";

        internal static string Behaviour = BaseLocation + "/Behaviour";
        internal static string WorldsLocations = BaseLocation + "/Worlds";
        internal static string GlobalConfig = BaseLocation + "/configs";
        static Serializer serializer = new Serializer();

        static YamlImporter (){
            Directory.CreateDirectory(BaseLocation);
            Directory.CreateDirectory(Behaviour);
            Directory.CreateDirectory(WorldsLocations);
            Directory.CreateDirectory(GlobalConfig);
        }

        internal static T Load<T>(string Location) {
            try {

                return serializer.Deserialize<T>(File.ReadAllText(Location));
            }catch(Exception e) {
                Console.WriteLine("Error loading file from {0}", Location);
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);

            }
            
        }
        internal static void Save(string Location, object Object) {
            string FileContent = serializer.Serialize(Object);
            File.WriteAllText(Location, FileContent);

        }

    }
}
