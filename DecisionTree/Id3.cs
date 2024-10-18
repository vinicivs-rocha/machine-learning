using System.Data;

namespace DecisionTree;

public class Id3<TNodeEdgeValue, TNodeFeatureValue>(DataTable trainingData) where TNodeFeatureValue : notnull
{
    public readonly Node<TNodeEdgeValue, TNodeFeatureValue> Root =
        Id3<TNodeEdgeValue, TNodeFeatureValue>.GetRootNode(trainingData);

    private static Node<TNodeEdgeValue, TNodeFeatureValue> GetRootNode(DataTable trainingData)
    {
        var rootNodeColumnIndex = int.MaxValue;
        var highestInformationGain = double.MinValue;
        var trainingDataEntropy = Id3<TNodeEdgeValue, TNodeFeatureValue>.CalculateTrainingDataEntropy(trainingData);
    }

    private static double CalculateTrainingDataEntropy(DataTable trainingData) =>
        Id3<TNodeEdgeValue, TNodeFeatureValue>.CalculateFeatureEntropy(trainingData, trainingData.Columns.Count - 1);

    private static double CalculateFeatureEntropy(DataTable trainingData, int featureIndex) =>
        (from DataRow row in trainingData.Rows
            let value = (TNodeFeatureValue)row[featureIndex]
            group value by value
            into groupedValues
            let labelCount = groupedValues.Count()
            let probability = (double)labelCount / trainingData.Rows.Count
            select -probability * Math.Log(probability, 2)).Sum();
}