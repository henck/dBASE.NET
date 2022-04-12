namespace dBASE.NET.Tests;

public static class AssertX
{
    /// <summary>
    /// Verifies that two objects are equal, using a default comparer.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be compared</typeparam>
    /// <param name="expected">The expected value</param>
    /// <param name="actual">The value to be compared against</param>
    /// <param name="userMessage">Message to show in the error</param>
    /// <exception cref="MyEqualException">Thrown when the objects are not equal</exception>
    public static void Equal<T>(T expected, T actual, string userMessage)
    {
        bool areEqual;

        if (expected == null || actual == null)
        {
            // If either null, equal only if both null
            areEqual = (expected == null && actual == null);
        }
        else
        {
            // expected is not null - so safe to call .Equals()
            areEqual = expected.Equals(actual);
        }

        if (!areEqual)
        {
            throw new MyEqualException(expected, actual, userMessage);
        }
    }
}