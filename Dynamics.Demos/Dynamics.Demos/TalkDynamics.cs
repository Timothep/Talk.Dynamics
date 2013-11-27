namespace Dynamics.Demos
{
    class TalkDynamics
    {
        public void DodnedderTreffen_20131128(dynamic thisTalk, dynamic thisGuy)
        {
            thisGuy.Name("Tim Bourguignon")
                   .Aka("Unsere Franzose")
                   .Employer("Mathema")
                   .Functions("TechLead", "SeniorDev", "Consultant", "Trainer").AndCo();

            var dynamicsInFreierWildbahn = thisTalk.Dynamisch().Und.Gefährlich();

            dynamicsInFreierWildbahn.GedankenSpiel("GO!");
        }
    }
}
