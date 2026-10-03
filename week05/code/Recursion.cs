using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it. If n <= 0, return 0.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1

        // Base case
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length 'size'
    /// from a list of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string word = "")
    {
        // TODO Start Problem 2

        // Base case: the required permutation length has been reached.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try each available letter.
        for (int i = 0; i < letters.Length; i++)
        {
            char chosenLetter = letters[i];

            // Remove the chosen letter so it cannot be reused
            // in the same permutation.
            string remainingLetters =
                letters[..i] + letters[(i + 1)..];

            // Recursively build the rest of the permutation.
            PermutationsChoose(
                results,
                remainingLetters,
                size,
                word + chosenLetter);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count the number of ways to climb stairs using
    /// steps of 1, 2, or 3. Uses memoization.
    /// </summary>
    public static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;

        if (s == 1)
            return 1;

        if (s == 2)
            return 2;

        if (s == 3)
            return 4;

        // TODO Start Problem 3

        // Create the memoization dictionary on the first call.
        remember ??= new Dictionary<int, decimal>();

        // If this value was already calculated, return it.
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Solve using recursion and pass the same dictionary
        // into every recursive call.
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Save the result for future recursive calls.
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Replace every wildcard * with every possible
    /// combination of 0 and 1.
    /// </summary>
    public static void WildcardBinary(
        string pattern,
        List<string> results)
    {
        // TODO Start Problem 4

        // Find the first wildcard.
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: there are no wildcards left.
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace this wildcard with 0.
        string withZero =
            pattern[..wildcardIndex] +
            "0" +
            pattern[(wildcardIndex + 1)..];

        // Replace this wildcard with 1.
        string withOne =
            pattern[..wildcardIndex] +
            "1" +
            pattern[(wildcardIndex + 1)..];

        // Recursively solve both possibilities.
        WildcardBinary(withZero, results);
        WildcardBinary(withOne, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0)
    /// and end at the 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function,
        // initialize the current path.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // TODO Start Problem 5

        // Stop if this position is outside the maze,
        // is a wall, or has already been visited in this path.
        if (!maze.IsValidMove(currPath, x, y))
        {
            return;
        }

        // Add the current position to the path.
        currPath.Add((x, y));

        // Base case: the end of the maze has been reached.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());

            // Backtrack before returning.
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Try all four directions recursively.

        // Left
        SolveMaze(results, maze, x - 1, y, currPath);

        // Right
        SolveMaze(results, maze, x + 1, y, currPath);

        // Up
        SolveMaze(results, maze, x, y - 1, currPath);

        // Down
        SolveMaze(results, maze, x, y + 1, currPath);

        // Backtrack so this square can be used in another possible path.
        currPath.RemoveAt(currPath.Count - 1);
    }
}