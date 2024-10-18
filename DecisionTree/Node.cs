namespace DecisionTree;

public record class Node<TEdgeValue, TFeatureValue>(
    string Name,
    bool IsLeaf,
    TEdgeValue Edge,
    Feature<TFeatureValue>? Feature
)
{
}