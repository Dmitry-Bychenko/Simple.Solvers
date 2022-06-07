namespace Simple.Solvers {

  //-------------------------------------------------------------------------------------------------------------------
  //
  /// <summary>
  /// Knapsack 1/0 Solution Item
  /// </summary>
  //
  //-------------------------------------------------------------------------------------------------------------------

  public sealed class KnapsackOneZeroItem<T> {
    #region Private Data
    #endregion Private Data

    #region Algorithm
    #endregion Algorithm

    #region Create

    internal KnapsackOneZeroItem(KnapsackOneZeroSolver<T> solver,
                                 T original,
                                 double weight,
                                 double value) {
      Solver = solver;
      Original = original;
      Weight = weight;
      Value = value;
    }

    #endregion Create

    #region Public

    /// <summary>
    /// Master
    /// </summary>
    public KnapsackOneZeroSolver<T> Solver { get; }

    /// <summary>
    /// Original Item
    /// </summary>
    public T Original { get; }

    /// <summary>
    /// Weight
    /// </summary>
    public double Weight { get; }

    /// <summary>
    /// Value
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// If item has been taken
    /// </summary>
    public bool Taken { get; internal set; }

    #endregion Public
  }

  //-------------------------------------------------------------------------------------------------------------------
  //
  /// <summary>
  /// Knapsack 1/0 Solver
  /// </summary>
  //
  //-------------------------------------------------------------------------------------------------------------------

  public sealed class KnapsackOneZeroSolver<T> {
    #region Private Data

    private readonly List<KnapsackOneZeroItem<T>> m_Items = new();

    #endregion Private Data

    #region Algorithm
    #endregion Algorithm

    #region Create

    /// <summary>
    /// Knapsack 1/0 solver
    /// </summary>
    /// <param name="items">Items to </param>
    /// <param name="maxWeight"></param>
    /// <param name="weightFunc">Weight Mapping Function</param>
    /// <param name="valueFunc">Value Mapping Function</param>
    /// <exception cref="ArgumentNullException">When items, weightFunc or valueFunc is null</exception>
    /// <exception cref="ArgumentException">When maxWeight is NaN</exception>
    public KnapsackOneZeroSolver(IEnumerable<T> items,
                                 double maxWeight,
                                 Func<T, double> weightFunc,
                                 Func<T, double> valueFunc) {
      if (items is null)
        throw new ArgumentNullException(nameof(items));

      if (double.IsNaN(maxWeight))
        throw new ArgumentException($"{maxWeight} must not be NaN", nameof(maxWeight));

      MaxWeight = maxWeight;

      WeightFunc = weightFunc ?? throw new ArgumentNullException(nameof(weightFunc));
      ValueFunc = valueFunc ?? throw new ArgumentNullException(nameof(valueFunc));

      foreach (var item in items)
        m_Items.Add(new(this, item, WeightFunc(item), ValueFunc(item)));
    }

    #endregion Create

    #region Public

    /// <summary>
    /// Maximum Weight
    /// </summary>
    public double MaxWeight { get; }

    /// <summary>
    /// Total Weight
    /// </summary>
    public double Weight => Items.Where(item => item.Taken).Sum(item => item.Weight);

    /// <summary>
    /// Total Value
    /// </summary>
    public double Value => Items.Where(item => item.Taken).Sum(item => item.Value);

    /// <summary>
    /// Weight Mapping Function
    /// </summary>
    public Func<T, double> WeightFunc { get; }

    /// <summary>
    /// Value Mapping Function
    /// </summary>
    public Func<T, double> ValueFunc { get; }

    /// <summary>
    /// Items
    /// </summary>
    public IReadOnlyList<KnapsackOneZeroItem<T>> Items => m_Items;

    /// <summary>
    /// To String (debug information)
    /// </summary>
    public override string ToString() {
      int count = Items.Count;
      int taken = Items.Count(item => item.Taken);

      return $"Items: {count} ({taken} taken) Value: {Value} Weight {Weight}";
    }

    #endregion Public
  }

}
