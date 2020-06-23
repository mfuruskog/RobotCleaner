using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCleaner
{
    public class Robot
    {
        public Coordinates Location { get; private set; }

        public Robot(Coordinates initialLocation)
        {
            Location = initialLocation;
        }


        public void Move(RobotCommand command)
        {
            switch (command.Direction)
            {
                case 'N':
                    Location.Y += command.Steps;
                    break;
                case 'E':
                    Location.X += command.Steps;
                    break;
                case 'S':
                    Location.Y -= command.Steps;
                    break;
                case 'W':
                    Location.X -= command.Steps;
                    break;
                default:
                    break;
            }
        }
    }
}
