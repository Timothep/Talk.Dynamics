namespace Dynamics.Demos
{
    class TalkDynamics
    {
        public void DodnedderTreffen_20131128(dynamic thisTalk, dynamic thisGuy)
        {
            thisGuy.Name("Tim Bourguignon")
                   .Aka("Unsere Franzose")
                   .Employer("Mathema")
                   .Functions(new [] {"TechLead", "SeniorDev", "Consultant", "Trainer" });

            var dynamischUndGefährlich = thisTalk.Dynamics("in").FreierWildbahn();

            dynamischUndGefährlich.Are.Those.Dynamics.Really().So.Bad("?");

            dynamischUndGefährlich.GedankenSpiel("GO!");
        }
    }
}
