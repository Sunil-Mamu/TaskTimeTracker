namespace TaskTimeTracker
{
    public class Task
    {
        public string Name { get; set; }
        public double Hours { get; set; }

        public Task(string name, double hours)
        {
            Name = name;
            Hours = hours;
        }

        public override string ToString() => $"{Name}: {Hours} hours";
    }
}
