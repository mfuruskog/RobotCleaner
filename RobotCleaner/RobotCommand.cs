namespace RobotCleaner
{
    public class RobotCommand
    {
        public RobotCommand(char direction, int steps)
        {
            Direction = direction;
            Steps = steps;
        }

        public char Direction { get; }
        public int Steps { get; }
    }
}
