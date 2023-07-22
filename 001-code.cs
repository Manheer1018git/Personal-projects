using System;

namespace SimpleReactionMachine
{
    class Tester
    {
        private static IController controller;
        private static IGui gui;
        private static string displayText;
        private static int randomNumber;
        private static int passed = 0;

        static void Main(string[] args)
        {
            // run simple test
            SimpleTest();
            Console.WriteLine("\n=====================================\nSummary: {0} tests passed out of 76", passed);
            Console.ReadKey();
        }

        private static void SimpleTest()
        {
            //Construct a ReactionController
            controller = new SimpleReactionController();
            gui = new DummyGui();

            //Connect them to each other
            gui.Connect(controller);
            controller.Connect(gui, new RndGenerator());

            //Reset the components()
            gui.Init();

            // *************************************************************
            // PART 1 - NORMAL GAME
            //Test the SimpleReactionController
            //IDLE
            DoReset('A', controller, "Insert coin");
            DoGoStop('B', controller, "Insert coin");
            DoTicks('C', controller, 1, "Insert coin");

            //coinInserted
            DoInsertCoin('D', controller, "Press GO!");

            //READY
            DoTicks('E', controller, 1, "Press GO!");
            DoInsertCoin('F', controller, "Press GO!");

            //goStop
            randomNumber = 117;
            DoGoStop('G', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('H', controller, randomNumber - 1, "Wait...");

            //RUN tick(s)
            DoTicks('I', controller, 1, "0.00");
            DoTicks('J', controller, 1, "0.01");
            DoTicks('K', controller, 11, "0.12");
            DoTicks('L', controller, 88, "1.00");

            //goStop
            DoGoStop('M', controller, "1.00");

            //STOP tick(s)
            DoTicks('N', controller, 299, "1.00");

            // game-2
            //tick
            randomNumber = 170;
            DoGoStop('O', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('P', controller, randomNumber - 1, "Wait...");

            //RUN tick(s)
            DoTicks('Q', controller, 1, "0.00");
            DoTicks('R', controller, 100, "1.00");

            //goStop
            DoGoStop('S', controller, "1.00");

            //STOP tick(s)
            DoTicks('T', controller, 299, "1.00");

            // game-3
            //tick
            randomNumber = 220;
            DoGoStop('U', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('V', controller, randomNumber - 1, "Wait...");

            //RUN tick(s)
            DoTicks('W', controller, 1, "0.00");
            DoTicks('X', controller, 100, "1.00");

            //goStop
            DoGoStop('Y', controller, "1.00");

            //STOP tick(s)
            DoTicks('Z', controller, 299, "1.00");

            //Reaction time displayed
            DoGoStop('a', controller, "Average reaction time: 1.00");
            DoTicks('b', controller, 499, "Average reaction time: 1.00");

            // new game ready to be started
            DoTicks('c', controller, 1, "Insert coin");

            // *************************************************************
            // PART 2 - 10 SECONDS AFTER COIN INSERTION
            //IDLE init
            gui.Init();
            DoReset('d', controller, "Insert coin");

            //coinInserted
            DoInsertCoin('e', controller, "Press GO!");

            //READY
            DoTicks('f', controller, 1, "Press GO!");
            DoTicks('g', controller, 998, "Press GO!");

            //TIMEOUT
            DoTicks('h', controller, 999, "Insert coin");

            // *************************************************************
            // PART 3 - WAIT: GOSTOP DURING GAME 1
            //IDLE init
            gui.Init();
            DoReset('i', controller, "Insert coin");

            //coinInserted
            DoInsertCoin('j', controller, "Press GO!");

            //READY
            DoTicks('k', controller, 1, "Press GO!");

            //goStop
            randomNumber = 117;
            DoGoStop('l', controller, "Wait...");
            DoTicks('f', controller, 50, "Wait...");

            //CHEATING - RESET
            DoGoStop('n', controller, "Insert coin");

            // *************************************************************
            // PART 4 - WAIT: GOSTOP DURING GAME 3
            //IDLE init
            gui.Init();
            DoReset('o', controller, "Insert coin");

            //coinInserted
            DoInsertCoin('p', controller, "Press GO!");

            //READY
            DoTicks('q', controller, 1, "Press GO!");

            //goStop
            randomNumber = 117;
            DoGoStop('r', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('s', controller, randomNumber - 1, "Wait...");

            //RUN tick(s)
            DoTicks('t', controller, 1, "0.00");
            DoTicks('u', controller, 100, "1.00");

            //goStop
            DoGoStop('v', controller, "1.00");

            //STOP tick(s)
            DoTicks('w', controller, 299, "1.00");

            // game-2
            //tick
            randomNumber = 170;
            DoGoStop('x', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('y', controller, randomNumber - 1, "Wait...");

            //RUN tick(s)
            DoTicks('z', controller, 1, "0.00");
            DoTicks('1', controller, 100, "1.00");

            //goStop
            DoGoStop('2', controller, "1.00");

            //STOP tick(s)
            DoTicks('3', controller, 299, "1.00");

            // game-3
            //tick
            randomNumber = 220;
            DoGoStop('4', controller, "Wait...");

            //CHEATING - RESET
            DoGoStop('5', controller, "Insert coin");

            // *************************************************************
            // PART 5 - IDLE -> READY init
            //IDLE init
            gui.Init();
            DoReset('6', controller, "Insert coin");

            randomNumber = 123;
            DoInsertCoin('7', controller, "Press GO!");
            // *********************************new game?	
            gui.Init();
            DoReset('8', controller, "Insert coin");

            // *************************************************************
            // PART 6 - IDLE -> READY -> WAIT init
            randomNumber = 123;
            DoInsertCoin('9', controller, "Press GO!");
            DoGoStop('A', controller, "Wait...");
            // *********************************new game?
            gui.Init();
            DoReset('B', controller, "Insert coin");

            // *************************************************************
            // PART 7 - IDLE -> READY -> WAIT -> RUN init
            randomNumber = 137;
            DoInsertCoin('C', controller, "Press GO!");
            DoGoStop('D', controller, "Wait...");
            DoTicks('E', controller, randomNumber + 98, "0.98");
            // *********************************new game?
            gui.Init();
            DoReset('F', controller, "Insert coin");

            // *************************************************************
            // PART 8 - IDLE -> READY -> WAIT -> RUN -> STOP init
            randomNumber = 119;
            DoInsertCoin('G', controller, "Press GO!");
            DoGoStop('H', controller, "Wait...");
            DoTicks('I', controller, randomNumber + 135, "1.35");
            DoGoStop('J', controller, "1.35");
            // *********************************new game?
            gui.Init();
            DoReset('K', controller, "Insert coin");

            // *************************************************************
            // PART 9 - IDLE -> READY -> WAIT -> RUN (timeout) -> STOP
            randomNumber = 120;
            DoInsertCoin('L', controller, "Press GO!");
            DoGoStop('M', controller, "Wait...");
            DoTicks('N', controller, randomNumber + 199, "1.99");
            DoTicks('O', controller, 50, "2.00");

            // *************************************************************            
        }

        private static void DoReset(char ch, IController controller, string msg)
        {
            try
            {
                controller.Init();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoGoStop(char ch, IController controller, string msg)
        {
            try
            {
                controller.GoStopPressed();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoInsertCoin(char ch, IController controller, string msg)
        {
            try
            {
                controller.CoinInserted();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoTicks(char ch, IController controller, int n, string msg)
        {
            try
            {
                for (int t = 0; t < n; t++) controller.Tick();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void GetMessage(char ch, string msg)
        {
            if (msg.ToLower() == displayText.ToLower())
            {
                Console.WriteLine("test {0}: passed successfully", ch);
                passed++;
            }
            else
                Console.WriteLine("test {0}: failed with message ( expected {1} | received {2})", ch, msg, displayText);
        }

        private class DummyGui : IGui
        {

            private IController controller;

            public void Connect(IController controller)
            {
                this.controller = controller;
            }

            public void Init()
            {
                displayText = "?reset?";
            }

            public void SetDisplay(string msg)
            {
                displayText = msg;
            }
        }

        private class RndGenerator : IRandom
        {
            public int GetRandom(int from, int to)
            {
                return randomNumber;
            }
        }

    }

}

