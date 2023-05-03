using Core.Enviroment.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enviroment {
    internal class WorldBehaviour {
        internal VoxelInfo[] Blocks;
        internal EnviromentInfo EnviromentInfo;
        internal VoxelProps[] Voxels = new VoxelProps[256];

        internal string BehaviourBasePath;

        internal WorldBehaviour(string EnviromentPath) {

            return;
            BehaviourBasePath = Path.Combine(YamlImporter.Behaviour, EnviromentPath);
            EnviromentInfo = YamlImporter.Load<EnviromentInfo>(Path.Combine(EnviromentPath, "WorldProps.yaml"));
            
        }
        internal void LoadVoxels() {
            string BlocksDirectory = Path.Combine(BehaviourBasePath, "Textures");
            string[] BlocksFiles = Directory.GetFiles(BlocksDirectory, "*.yaml");

            foreach (string BlockFile in BlocksFiles) {
                string YAMLContent = File.ReadAllText(BlockFile);
                VoxelInfo vi = YamlImporter.Load<VoxelInfo>(BlockFile);

                if(vi.ID < 0 || vi.ID > 255) {

                    Console.WriteLine(BlockFile + " incorrect ID, the ID must fit in a unsigned byte(0-255)");
                    continue;
                }

                VoxelProps VP = new VoxelProps();
                VP.VoxelID = vi.ID;
                VP.IsTransparent = vi.Transparent;
                //VP.
            }

        }
        
    }
}
