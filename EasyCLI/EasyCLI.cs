using System;

namespace EasyCliLib
{
    public class EasyCLI
    {
        #region Public Interface
        public static EasyCLI Create()
        {
            return new EasyCLI();
        }

        public void Run()
        {
            Console.ReadKey();
        }
        #endregion

        #region Private
        private EasyCLI() { }
        #endregion
    }
}
