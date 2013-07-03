namespace Dynamics.Demos
{
    internal class Calculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }

    /// <summary>
    /// Demo heavily inspired by Scott Hanselmann's Blog post
    /// http://www.hanselman.com/blog/C4AndTheDynamicKeywordWhirlwindTourAroundNET4AndVisualStudio2010Beta1.aspx
    /// </summary>
    internal class CalcDemo
    {
        public CalcDemo()
        {
            //>>>>Default

            Calculator calc = GetCalculator();
            int sum1 = calc.Add(10, 20);

            //>>>>Doesn't compile

            //object objectCalc = GetCalculator();
            //int sum = objectCalc.Add(10, 20);

            //>>>>Reflection

            //object reflectionCalc = GetCalculator();
            //Type calcType = reflectionCalc.GetType();
            //object res = calcType.InvokeMember(
            //    "Add", 
            //    BindingFlags.InvokeMethod,
            //    null,
            //    Activator.CreateInstance(calcType),
            //    new object[] { 10, 20 });
            //int sum2 = Convert.ToInt32(res);

            //>>>>Dynamic

            //dynamic dynamicCalc = GetCalculator();
            //int sum3 = dynamicCalc.Add(10, 20);
        }

        public Calculator GetCalculator()
        {
            return new Calculator();
        }
    }
}