using ScpDriverInterface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Halo_5_Server_Looking_for_Group
{
    class XboxCommands
    {
        const int CONTROLLER_NUMBER = 1;

        ScpBus scpbus = null;
        X360Controller controller = new X360Controller();

        public XboxCommands()
        {
            try
            {
                scpbus = new ScpBus();

            }
            catch (Exception ex)
            {
                Console.WriteLine("SCP Bus failed to initialize");
                Console.WriteLine(ex.ToString());
            }
           // scpbus.UnplugAll();
           // Thread.Sleep(3000);

            scpbus.PlugIn(CONTROLLER_NUMBER);
            Thread.Sleep(3000);
        }

        ~XboxCommands()
        {
            //clean up since it's buggy
            scpbus.Unplug(CONTROLLER_NUMBER);
        }
        public void SendInvite(string gamertag)
        {
            ClickButton(X360Buttons.Logo, 50, 1500);

            ClickButton(X360Buttons.Left, 50, 500);

            ClickButton(X360Buttons.Left, 50, 500);

            ClickButton(X360Buttons.A, 50, 100);

            Thread.Sleep(1000);
            foreach (char ch in gamertag.ToCharArray())
            {
                CharacterToVirtualKeyboard(ch);
            }

            ClickButton(X360Buttons.Start, 50, 700);

            ClickButton(X360Buttons.Down, 50, 400);
            ClickButton(X360Buttons.A, 50, 400);
            ClickButton(X360Buttons.Down, 50, 500);
            ClickButton(X360Buttons.A, 50, 500);

            ClickButton(X360Buttons.Logo, 50, 500);
            ClickButton(X360Buttons.A, 50, 500);
        }
        //todo: fix loops
        private void CharacterToVirtualKeyboard(char ch)
        {
            SendtoController(controller);

            //6, [y], h, n
            //every char is respect to 'y'
            char[] numrow = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] toprow = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
            char[] middlerow = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' };
            char[] bottomrow = { 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            int pos = 0;
            foreach (char c in numrow)
            {
                if(c == ch)
                {
                    ClickButton(X360Buttons.Up, 50, 500);
                    MoveToKey(pos);
                    ClickButton(X360Buttons.Down, 50, 500);

                    return;
                }
                pos++;
            }

            pos = 0;
            foreach (char c in toprow)
            {
                if (c == ch)
                {
                    MoveToKey(pos);

                    return;
                }
                pos++;
            }

            pos = 0;
            foreach (char c in middlerow)
            {
                if (c == ch)
                {
                    ClickButton(X360Buttons.Down, 50, 500);
                    MoveToKey(pos);
                    ClickButton(X360Buttons.Up, 50, 500);

                    return;
                }
                pos++;
            }

            pos = 0;
            foreach (char c in bottomrow)
            {
                if (c == ch)
                {
                    ClickButton(X360Buttons.Down, 50, 500);
                    ClickButton(X360Buttons.Down, 50, 500);
                    MoveToKey(pos);
                    ClickButton(X360Buttons.Up, 50, 500);
                    ClickButton(X360Buttons.Up, 50, 500);

                    return;
                }
                pos++;
            }
        }

        private void MoveToKey(int pos)
        {
            const int STARTPOS = 5;

            int diff = STARTPOS - pos;
            if (diff >= 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    ClickButton(X360Buttons.Left, 80, 500);
                }

                ClickButton(X360Buttons.A, 50, 500);

                for (int i = 0; i < diff; i++)
                {
                    ClickButton(X360Buttons.Right, 80, 500);
                }
            }
            else
            {
                diff = diff * -1;
                for (int i = 0; i < diff; i++)
                {
                    ClickButton(X360Buttons.Right, 80, 500);
                }

                ClickButton(X360Buttons.A, 80, 500);

                for (int i = 0; i < diff; i++)
                {
                    ClickButton(X360Buttons.Left, 80, 500);
                }
            }
        }
        private void ClickButton(X360Buttons button, int down, int msleep)
        {
            Console.WriteLine("Clicking: " + button.ToString() + " down: " + down + " sleep " + msleep);
            controller.Buttons = button;
            SendtoController(controller);

            Thread.Sleep(down);

            //controller.Buttons &= ~button;
            controller.Buttons = X360Buttons.None;
            SendtoController(controller);

            Thread.Sleep(msleep);
        }

        public void SendtoController(X360Controller controller)
        {

            byte[] report = controller.GetReport();
            byte[] output = new byte[8];

            scpbus.Report(CONTROLLER_NUMBER, report);
        }
    }
}
