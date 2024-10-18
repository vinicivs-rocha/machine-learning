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
}