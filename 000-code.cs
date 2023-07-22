// we have used MealyMoore FSM in this task
using System;
namespace SimpleReactionMachine
{
    // (1) we declare an enum for the different states
    enum State
    {
        Run,
        Ready,
        Wait,
        Reaction,
        Gamestop,
        Results
    }

    // our class inherits the IController interface
    public class SimpleReactionController : IController
    {
        // (2) we declare the state variable
        private State _state;

        // (3) we declare the gloabal variable and the constants
        private const int GAME_TIME = 200;
        private const int RESULT_TIME = 300;
        private const int READY_TIME = 1000;
        private const int RESULT_DISPLAY_TIME = 500;
        private const double TICKS_IN_A_SECOND = 100.0;
        private IGui _gui;
        private IRandom _random;
        private int _ticks;
        private int _rndtime;
        private int _games;
        private double _reactiontime;
        private double _reactiontime1;
        private double _reactiontime2;
        private double _reactiontime3;

        // (4) we make a method to connect the interfaces IGui and IRandom and use it in a way of constructor
        public void Connect(IGui gui, IRandom rng)
        {
            _gui = gui;
            _random = rng;
        }

        // (5) Initialisation routine
        public void Init()
        {
            Next(State.Run);
        }

        // (6) we make an event handler for when the user inserts coin
        public void CoinInserted()
        {
            switch (_state)
            {
                case State.Run:
                    Next(State.Ready);
                    return;
                default:
                    return;
            }
        }

        // (7) we make an event handler for when user presses go/stop 
        public void GoStopPressed()
        {
            switch (_state)
            {
                case State.Ready:
                    Next(State.Wait);
                    return;
                case State.Wait:
                    Next(State.Run);
                    return;
                case State.Reaction:
                    _games++;
                    Next(State.Gamestop);
                    return;
                case State.Gamestop:
                    if (_games == 3)
                    {
                        Next(State.Results);
                        return;
                    }
                    else
                    {
                        Next(State.Wait);
                    }
                    return;
                case State.Results:
                    Next(State.Run);
                    return;
                default:
                    return;
            }
        }

        // (8) we make a method which handles the ticks which in our case represents 10 milliseconds
        public void Tick()
        {
            switch (_state)
            {
                case State.Ready:
                    _ticks++;
                    if (_ticks == READY_TIME)
                    {
                        Next(State.Run);
                    }
                    return;
                case State.Wait:
                    _ticks++;
                    if (_ticks == _rndtime)
                    {
                        Next(State.Reaction);
                    }
                    return;
                case State.Reaction:
                    _ticks++;
                    _gui.SetDisplay((_ticks / TICKS_IN_A_SECOND).ToString("0.00"));
                    if (_ticks == GAME_TIME)
                    {
                        if (_games == 0)
                        {
                            _reactiontime1 = (_ticks / TICKS_IN_A_SECOND);
                        }
                        else if (_games == 1)
                        {
                            _reactiontime2 = (_ticks / TICKS_IN_A_SECOND);
                        }
                        else if (_games == 2)
                        {
                            _reactiontime3 = (_ticks / TICKS_IN_A_SECOND);
                        }
                        _games++;
                        Next(State.Gamestop);
                    }
                    if (_games == 0)
                    {
                        _reactiontime1 = (_ticks / TICKS_IN_A_SECOND);
                    }
                    else if (_games == 1)
                    {
                        _reactiontime2 = (_ticks / TICKS_IN_A_SECOND);
                    }
                    else if (_games == 2)
                    {
                        _reactiontime3 = (_ticks / TICKS_IN_A_SECOND);
                    }
                    return;
                case State.Gamestop:
                    _ticks++;
                    if (_ticks == RESULT_TIME)
                    {
                        if (_games == 3)
                        {
                            Next(State.Results);
                            return;
                        }
                        else
                        {
                            Next(State.Wait);
                        }
                    }
                    return;
                case State.Results:
                    _ticks++;
                    if (_ticks == RESULT_DISPLAY_TIME)
                    {
                        Next(State.Run);
                    }
                    return;
                default:
                    return;
            }
        }

        // (9) we make a method which handles actions on entry to a state
        void Next(State state)
        {
            _state = state;
            _ticks = 0;

            switch (_state)
            {
                case State.Run:
                    _games = 0;
                    _reactiontime = 0;
                    _reactiontime1 = 0;
                    _reactiontime2 = 0;
                    _reactiontime3 = 0;
                    _rndtime = 0;
                    _gui.SetDisplay("Insert coin");
                    return;
                case State.Ready:
                    _gui.SetDisplay("Press GO!");
                    return;
                case State.Wait:
                    _rndtime = _random.GetRandom(100, 250);
                    _gui.SetDisplay("Wait...");
                    return;
                case State.Reaction:
                    _gui.SetDisplay("0.00");
                    return;
                case State.Results:
                    _reactiontime = _reactiontime1 + _reactiontime2 + _reactiontime3;
                    _gui.SetDisplay("Average reaction time: " + (_reactiontime / _games).ToString("0.00"));
                    _ticks = 0;
                    return;
                default:
                    return;
            }
        }
    }
}
