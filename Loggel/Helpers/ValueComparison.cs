namespace Loggel.Helpers
{
  public class ValueComparison
  {
    //-------------------------------------------------------------------------

    public enum Comparison
    {
      UNKNOWN,
      EQUAL,
      NOT_EQUAL,
      GREATER,
      LESSER,
      IN_RANGE,
      NOT_IN_RANGE
    }

    //-------------------------------------------------------------------------

    public static string ComparisonAsString( Comparison comparison )
    {
      switch( comparison )
      {
        case Comparison.EQUAL:
          return "==";

        case Comparison.NOT_EQUAL:
          return "!=";

        case Comparison.GREATER:
          return ">";

        case Comparison.LESSER:
          return "<";

        case Comparison.IN_RANGE:
          return "()";

        case Comparison.NOT_IN_RANGE:
          return "!()";

        default:
          return null;
      }
    }

    //-------------------------------------------------------------------------

    public static Comparison ComparisonFromString( string s )
    {
      Comparison comparison = Comparison.UNKNOWN;

      switch( s )
      {
        case "==":
          comparison = Comparison.EQUAL;
          break;

        case "!=":
          comparison = Comparison.NOT_EQUAL;
          break;

        case ">":
          comparison = Comparison.GREATER;
          break;

        case "<":
          comparison = Comparison.LESSER;
          break;

        case "()":
          comparison = Comparison.IN_RANGE;
          break;

        case "!()":
          comparison = Comparison.NOT_IN_RANGE;
          break;

        default:
          comparison = Comparison.UNKNOWN;
          break;
      }

      return comparison;
    }

    //-------------------------------------------------------------------------

    public static bool Compare(
      dynamic value1,
      dynamic value2,
      Comparison comparison )
    {
      bool result = false;

      try
      {
        switch( comparison )
        {
          case Comparison.EQUAL:
            result = ( value1 == value2 );
            break;

          case Comparison.NOT_EQUAL:
            result = ( value1 != value2 );
            break;

          case Comparison.GREATER:
            result = ( value1 > value2 );
            break;

          case Comparison.LESSER:
            result = ( value1 < value2 );
            break;
        }
      }
      catch
      {
        // Ignore - we're happy for the comparison simply to fail.
      }

      return result;
    }

    //-------------------------------------------------------------------------

    public static bool Compare(
      dynamic value,
      dynamic rangeStart,
      dynamic rangeEnd,
      Comparison comparison )
    {
      bool result = false;

      try
      {
        switch( comparison )
        {
          case Comparison.IN_RANGE:
            result = ( rangeStart <= value && value <= rangeEnd );
            break;

          case Comparison.NOT_IN_RANGE:
            result = ( value < rangeStart || value > rangeEnd );
            break;
        }
      }
      catch
      {
        // Ignore - we're happy for the comparison simply to fail.
      }

      return result;
    }

    //-------------------------------------------------------------------------
  }
}
