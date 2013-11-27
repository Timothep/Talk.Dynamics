namespace Dynamics.Demos
{
    class TalkDynamics
    {
        public void DodnedderTreffen_20131128(dynamic thisTalk, dynamic thisGuy)
        {
            var dynamicsInFreierWildbahn = thisTalk.Dynamisch().Und.Gefährlich();

            thisGuy.Name("Tim Bourguignon")
                   .Aka("Unsere Franzose")
                   .Employer("Mathema")
                   .Functions("TechLead", "SeniorDev", "Consultant", "Trainer");

            dynamicsInFreierWildbahn.GedankenSpiel("GO!");
        }
    }
}
