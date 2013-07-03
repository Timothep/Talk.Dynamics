namespace Dynamics.Demos
{
    internal class FirstDemo
    {
        public FirstDemo()
        {
            DoesNotCompile();
            DynamicsToTheRescue();
        }

        public static void DoesNotCompile()
        {
            //string lang = "C#";
            //lang++;

            //int theAnswer = 42;
            //theAnswer.ToUpper();
        }

        public static void DynamicsToTheRescue()
        {
            dynamic lang = "C#";
            lang++;

            dynamic theAnswer = 42;
            theAnswer.ToUpper();
        }
    }
}