namespace Escc.Umbraco.PickupAndSendEmails.Tests
{
    internal class FakeSubjectParser : ISubjectParser
    {
        public int[] LocateSubjectHeader(string text)
        {
            return new int[2];
        }

        public string ParseSubject(string text)
        {
            return "Fake subject";
        }
    }
}