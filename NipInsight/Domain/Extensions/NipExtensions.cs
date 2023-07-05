namespace NipInsight.Domain.Extensions;

public static class NipExtensions
{
    public static bool IsValidNip(this string nip)
    {
        if (string.IsNullOrEmpty(nip))
        {
            return false;
        }

        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

        var isDigit = nip.All(char.IsDigit);

        if (isDigit)
        {
            var controlSum = CalculateControlSum(nip, weights);
            var controlNum = controlSum % 11;

            if (controlNum == 10)
            {
                return false;
            }

            var lastDigit = int.Parse(nip[^1].ToString());

            return controlNum == lastDigit;
        }

        return false;
    }

    private static int CalculateControlSum(string nip, IReadOnlyList<int> weights)
    {
        var controlSum = 0;

        for (var i = 0; i < weights.Count; i++)
        {
            controlSum += weights[i] * int.Parse(nip[i].ToString());
        }

        return controlSum;
    }
}