using Core;

namespace DecisionTree;

public record class Node<TFeatureValue, TDataValue>(
  Feature<TFeatureValue> Feature,
  List<TDataValue> Data,
  List<Node<TFeatureValue, TDataValue>> Children,
  List<double>? ClassProbabilities = null
)
  where TFeatureValue : IComparable<TFeatureValue>
  where TDataValue : IComparable<TDataValue>
{ 
  public bool IsLeaf => ClassProbabilities is not null;
  public double FeatureImportance => Feature.Importance(Data);
}