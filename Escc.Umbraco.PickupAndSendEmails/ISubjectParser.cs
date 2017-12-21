namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Parses the subject from a serialised email
    /// </summary>
    public interface ISubjectParser
    {
        /// <summary>
        /// Parses the subject.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string ParseSubject(string text);
    }
}