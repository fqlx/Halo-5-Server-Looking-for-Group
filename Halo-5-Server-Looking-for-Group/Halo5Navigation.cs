using ScpDriverInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halo_5_Server_Looking_for_Group
{
    class Halo5Navigation : XboxNavigation
    {
        public void SelectCustomGameOnLaunch()
        {
            ClickButton(X360Buttons.Start, 40, 7000);

            ClickButton(X360Buttons.B, 40, 4000);
            ClickButton(X360Buttons.B, 40, 4000);
           // ClickButton(X360Buttons.B, 40, 2000);

            ClickButton(X360Buttons.Down, 80, 500);
            ClickButton(X360Buttons.Right, 40, 500);
            ClickButton(X360Buttons.Right, 40, 500);
            ClickButton(X360Buttons.A, 80, 500);
        }

        //Everything is done is respect to 'Map'

        //TODO:  SelectMap() should be done with JSON file
        public void SelectMapAlpine()
        {
            ClickButton(X360Buttons.A, 80, 500);
            ClickButton(X360Buttons.A, 80, 500);
            ClickButton(X360Buttons.A, 80, 500);
        }

        public void SelectModeFFARockets()
        {
            ClickButton(X360Buttons.Down, 80, 500);

            ClickButton(X360Buttons.A, 80, 500);
            ClickButton(X360Buttons.A, 80, 500);

            for (int i = 0; i < 7; i++)
                ClickButton(X360Buttons.Down, 80, 500);

            ClickButton(X360Buttons.A, 80, 500);

            ClickButton(X360Buttons.Up, 80, 500);
        }

        public void StartGame()
        {
            for (int i = 0; i < 3; i++)
                ClickButton(X360Buttons.Down, 80, 500);

            ClickButton(X360Buttons.A, 80, 500);
        }
    }
}
