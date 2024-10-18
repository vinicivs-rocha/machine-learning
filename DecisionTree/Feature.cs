using System.Data;

namespace DecisionTree;

/// <summary>
/// Represents a feature in a decision tree.
/// </summary>
/// <typeparam name="TValue">The type of the values for the feature.</typeparam>
/// <param name="Name">The name of the feature.</param>
/// <param name="Values">The list of possible values for the feature.</param>
public record class Feature<TValue>(string Name, double InformationGain, List<TValue> Values)
{
    public static List<TValue> GetDifferentValues(DataTable trainingData, int featureIndex) =>
        trainingData.AsEnumerable().Select(row => (TValue)row[featureIndex]).Distinct().ToList();

    private static double CalculateEntropy(DataTable trainingData, int featureIndex) =>
        (from DataRow row in trainingData.Rows
            let value = (TValue)row[featureIndex]
            group value by value
            into groupedValues
            let labelCount = groupedValues.Count()
            let probability = (double)labelCount / trainingData.Rows.Count
            select -probability * Math.Log(probability, 2)).Sum();

    private static double CalculateWeightedAverages(DataTable trainingData, int featureIndex)
    {
        var featureEntropy = Feature<TValue>.CalculateEntropy(trainingData, featureIndex);

        return (from DataRow row in trainingData.Rows
            let value = (TValue)row[featureIndex]
            group value by value
            into groupedValues
            let labelCount = groupedValues.Count()
            let probability = (double)labelCount / trainingData.Rows.Count
            select probability * featureEntropy).Sum();
    }
    
    public static double CalculateInformationGain(DataTable trainingData, int featureIndex)
    {
        var parentEntropy = Feature<TValue>.CalculateEntropy(trainingData, trainingData.Columns.Count - 1);
        var weightedAverages = Feature<TValue>.CalculateWeightedAverages(trainingData, featureIndex);

        return parentEntropy - weightedAverages;
    }
}