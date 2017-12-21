namespace Escc.Umbraco.PickupAndSendEmails
{
    /// <summary>
    /// Parses the subject from a serialised email
    /// </summary>
    public interface ISubjectParser
    {
        /// <summary>
        /// Locates the subject header and returns the start and end position in the string
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        (int, int) LocateSubjectHeader(string text);

        /// <summary>
        /// Parses the subject.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string ParseSubject(string text);
    }
}