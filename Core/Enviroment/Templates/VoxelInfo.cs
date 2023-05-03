using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enviroment.Templates {
    [System.Serializable]
    internal class VoxelInfo {
        public int ID { get; set; } = 0;
        public bool Transparent { get; set; } = false;
        public string[] TextureFaces { get; set; } = new string[6] { "path", "path", "path", "path", "path" , "path" };

    }
}
