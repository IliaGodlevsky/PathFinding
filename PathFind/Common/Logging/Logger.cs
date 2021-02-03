using Common.Base;

namespace Common
{
    public sealed class Logger : BaseLogger
    {
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        private Logger(): base()
        {

        }

        private static Logger instance = null;
    }
}
