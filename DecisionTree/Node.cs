namespace DecisionTree;

public abstract record class Node<TEdgeValue, TFeatureValue>(
    string Name,
    bool IsLeaf,
    TEdgeValue? Edge,
    List<Node<TEdgeValue, TFeatureValue>> Children,
    Feature<TFeatureValue>? Feature
)
{
}