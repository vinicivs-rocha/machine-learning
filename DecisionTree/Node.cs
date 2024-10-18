namespace DecisionTree;

public record class Node<TEdgeValue, TFeatureValue>(
    bool IsLeaf,
    TEdgeValue Edge,
    Feature<TFeatureValue>? Feature,
    List<Node<TEdgeValue, TFeatureValue>> Children
)
{
}