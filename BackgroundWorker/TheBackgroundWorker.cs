using System.Reflection;

namespace BackgroundWorker
{
    public static class TheBackgroundWorker
    {
        public static Assembly Assembly => typeof(TheBackgroundWorker).GetTypeInfo().Assembly;
    }
}
