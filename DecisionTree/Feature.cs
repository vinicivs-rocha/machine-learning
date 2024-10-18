using System.Data;

namespace DecisionTree;

/// <summary>
/// Represents a feature in a decision tree.
/// </summary>
/// <typeparam name="TValue">The type of the values for the feature.</typeparam>
/// <param name="Name">The name of the feature.</param>
/// <param name="Values">The list of possible values for the feature.</param>
public abstract record class Feature<TValue>(string Name, List<TValue> Values)
{
    public static List<TValue> GetDifferentValues(DataTable data, int featureIndex) =>
        data.AsEnumerable().Select(row => (TValue)row[featureIndex]).Distinct().ToList();
}