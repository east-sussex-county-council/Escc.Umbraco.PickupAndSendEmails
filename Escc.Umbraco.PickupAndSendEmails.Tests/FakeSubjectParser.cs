namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    internal class FakeSubjectParser : ISubjectParser
    {
        public string ParseSubject(string text)
        {
            return "Fake subject";
        }
    }
}