﻿namespace Dynamics.Demos
{
    internal class DummyDemo
    {
        public static void WillItCompile()
        {
            //string lang = "C#";
            //lang++;
            //
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

        public static void WTF(dynamic magic)
        {
            magic.WhatTheFuck().HowDoesThisWork();
        }
    }
}