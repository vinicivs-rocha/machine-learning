using System.Data;

namespace DecisionTree;

public class Id3<TNodeEdgeValue, TNodeFeatureValue>(DataTable trainingData)
    where TNodeFeatureValue : notnull
    where TNodeEdgeValue : notnull
{
    public readonly Node<TNodeEdgeValue, TNodeFeatureValue> Root =
        Id3<TNodeEdgeValue, TNodeFeatureValue>.GetBestNode(trainingData, trainingData.Columns.Count - 1);

    private static Node<TNodeEdgeValue, TNodeFeatureValue> GetBestNode(DataTable parentData, int parentColumnIndex,
        TNodeEdgeValue edgeValue = default!)
    {
        var features = parentData.Columns.Cast<DataColumn>().Select(column =>
            new Feature<TNodeFeatureValue>(column.ColumnName,
                Feature<TNodeFeatureValue>.CalculateInformationGain(parentData, column.Ordinal),
                Feature<TNodeFeatureValue>.GetDifferentValues(parentData, column.Ordinal)));
        var bestFeature = features.Aggregate((best, current) =>
            current.InformationGain > best.InformationGain ? current : best);

        return new Node<TNodeEdgeValue, TNodeFeatureValue>(IsLeaf: false, Edge: edgeValue, Feature: bestFeature,
            Children: []);
    }
}