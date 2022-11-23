namespace Pathfinding.App.Console.ValueInput.ProgrammedInput.FromFile
{
    internal sealed class FromFileProgrammedStringInput : FromFileProgrammedInput<string>
    {
        public FromFileProgrammedStringInput() : base("Script_String.txt")
        {
        }

        protected override bool Parse(string value, out string output)
        {
            output = value;
            return true;
        }
    }
}
