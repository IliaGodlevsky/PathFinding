namespace ConsoleVersion.ValueInput.ProgrammedInput.FromFile
{
    internal sealed class FromFileProgrammedIntInput : FromFileProgrammedInput<int>
    {
        public FromFileProgrammedIntInput() : base("Script_int.txt")
        {
        }

        protected override bool Parse(string value, out int output)
        {
            return int.TryParse(value, out output);
        }
    }
}
