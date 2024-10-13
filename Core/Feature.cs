namespace Core;

public record class Feature<TValue>(int Index, List<TValue> Labels, double InformationGain) where TValue : IComparable<TValue>
{
  public static List<TValue> GetFeatureValuesFromDataset(List<List<TValue>> data, int featureIndex) =>
    data.Select(row => row[featureIndex]).Distinct().ToList();

  public double Entropy =>
    CalculateClassProbabilities().Sum(probability => -probability * Math.Log(probability, 2));

  public List<double> CalculateClassProbabilities() =>
    Labels.Select(label => (double)Labels.Count(otherLabel => otherLabel.Equals(label)) / Labels.Count).ToList();

  public double Importance<TDataValue>(List<TDataValue> data) => data.Count - InformationGain;
}
