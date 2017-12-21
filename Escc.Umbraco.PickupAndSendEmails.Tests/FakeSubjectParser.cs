namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    internal class FakeSubjectParser : ISubjectParser
    {
        public (int, int) LocateSubjectHeader(string text)
        {
            return (0,0);
        }

        public string ParseSubject(string text)
        {
            return "Fake subject";
        }
    }
}