using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Graded_Unit_research_4
{
    /// <summary>
    /// A class which holds details of player skills
    /// Grant Shaw
    /// 01/05/2019
    /// </summary>

    class Player
    {
        //declaring variables for player object , which holds data for player record before moving it to Database
        private DateTime update;
        private int std;
        private int spin;
        private int pop;
        private int front;
        private int rear;
        private int side;
        private int scrabble;
        private int drop;
        private int punt;
        private int grubber;
        private int goal;



        //Validation checks on the information entered into text boxes on the PlayerProfileSheet.
        public DateTime Update
        {
            get { return update; }

            set
            {

                if (value != null)
                {
                    update = value;
                }
                else
                    throw new Exception("Please select a valid date");
            }

        }

        
        public int Std
        {
            get { return std; }

            set
            {
                              
                if (value >= 0 && value <= 5)
                {
                    
                    std = value;
                }
                else throw new Exception("Standard must be rated between 0-5");
            }
        }

        public int Spin
        {
            get { return spin; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    spin = value;
                }
                else throw new Exception("Spin must be rated between 0-5");
            }
        }

        public int Pop
        {
            get { return pop; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    pop = value;
                }
                else throw new Exception("Pop must be rated between 0-5");
            }
        }
        public int Front
        {
            get { return front; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    front = value;
                }
                else throw new Exception("front must be rated between 0-5");
            }
        }
        public int Rear
        {
            get { return rear; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    rear = value;
                }
                else throw new Exception("Rear must be rated between 0-5");
            }
        }
        public int Side
        {
            get { return side; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    side = value;
                }
                else throw new Exception("Side must be rated between 0-5");
            }
        }
        public int Scrabble
        {
            get { return scrabble; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    scrabble = value;
                }
                else throw new Exception("Scrabble must be rated between 0-5");
            }
        }

        public int Drop
        {
            get { return drop; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    drop = value;
                }
                else throw new Exception("Spin must be rated between 0-5");
            }
        }

        public int Punt
        {
            get { return punt; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    punt = value;
                }
                else throw new Exception("Spin must be rated between 0-5");
            }
        }
        public int Grubber
        {
            get { return grubber; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                    grubber = value;
                }
                else throw new Exception("Spin must be rated between 0-5");
            }
        }

        public int Goal
        {
            get { return goal; }

            set
            {
                if (value >= 0 && value <= 5)
                {
                     goal = value;
                }
                else throw new Exception("Spin must be rated between 0-5");
            }
        }


        //Constructor
        public Player(DateTime updte, int Standard1, int Spin1, int pop1 , int front1 , int rear1, int side1 , int scrabble1 , int drop1, int punt1 , int grubber1, int goal1)
        {
            this.Update = updte;
            this.Std = Standard1;
            this.Spin = Spin1;
            this.Pop = pop1;
            this.Front = front1;
            this.Rear = rear1;
            this.Side = side1;
            this.Scrabble = scrabble1;
            this.Drop = drop1;
            this.Punt = punt1;
            this.Grubber = grubber1;
            this.Goal = goal1;


        }

        //A constructor which removes white space from strings.
        private string RemoveWhiteSpace(string stuff)
        {
            for (int i = 0; i <= stuff.Length; i++)
            {
                stuff = Regex.Replace(stuff, @"\s+", "");

            }

            return stuff;

        }





    }
}
