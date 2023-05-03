using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Player {
    internal class PlayerController {

        internal MovmentController MovmentController = new MovmentController();

        internal void Update() {
            MovmentController.Update();
            //Gui elements and more
        }

    }
}
