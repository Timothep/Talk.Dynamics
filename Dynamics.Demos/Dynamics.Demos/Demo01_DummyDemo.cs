namespace Dynamics.Demos
{
    internal class DummyDemo
    {

        public void WillItCompile()
        {
            //string lang = "C#";
            //lang++;
            //
            //int theAnswer = 42;
            //theAnswer.ToUpper();
        }












        public void DynamicsToTheRescue()
        {
            dynamic lang = "C#";
            lang++;

            dynamic theAnswer = 42;
            theAnswer.ToUpper();
        }











        public void WTF(dynamic magic)
        {
            magic.WhatTheFuck().HowDoesThisWork();
        }




    }
}