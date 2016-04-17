﻿using System.Collections.Generic;
using System.Xml;
using Loggel.Helpers;

namespace Loggel.Processors
{
  public class And : Processor
  {
    //-------------------------------------------------------------------------

    public struct Condition
    {
      public CircuitContext ValueSource { get; set; }
      public dynamic ComparisonValue { get; set; }
      public ValueComparison.Comparison ComparisonType { get; set; }
    }

    //-------------------------------------------------------------------------

    // This processor's output socket.
    public Socket OutputSocket { get; set; }

    // List of conditions's which we'll 'and' when processing.
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    //-------------------------------------------------------------------------

    public And(
      uint id,
      string name,
      string description,
      CircuitContext circuitContext )
    :
      base( id, name, description, circuitContext )
    {
      OutputSocket =
        CreateOutputSocket(
          "Result",
          "Will be live when AND condition is satisfied." );
    }

    //-------------------------------------------------------------------------
    
    // If all the conditions are met then we can return the connected
    // processor (if there is one) as the next processor to be processed.

    public override Processor Process()
    {
      Processor nextProcessor = null;
      bool allConditionsMet = true;

      // Evaluate each condition.
      foreach( Condition condition in Conditions )
      {
        bool result =
          ValueComparison.Compare(
            condition.ValueSource.Value,
            condition.ComparisonValue,
            condition.ComparisonType );

        if( result == false )
        {
          allConditionsMet = false;
          break;
        }
      }

      if( allConditionsMet )
      {
        nextProcessor = OutputSocket.ConnectedProcessor;
      }

      return nextProcessor;
    }

    //-------------------------------------------------------------------------

    // Persist this instance as XML.

    public override XmlElement GetAsXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.GetAsXml( parent );

      // 'And' processor xml.
      XmlDocument ownerDoc = parent.OwnerDocument;
      XmlElement andElement = ownerDoc.CreateElement( "And" );
      parent.AppendChild( andElement );

      // Conditions.
      XmlElement conditionCollection = ownerDoc.CreateElement( "ConditionCollection" );
      andElement.AppendChild( conditionCollection );

      foreach( Condition condition in Conditions )
      {
        XmlElement conditionElement = ownerDoc.CreateElement( "Condition" );
        conditionCollection.AppendChild( conditionElement );

        XmlElement valueSourceIdElement = ownerDoc.CreateElement( "ValueSourceId" );
        conditionElement.AppendChild( valueSourceIdElement );
        if( condition.ValueSource != null )
        {
          valueSourceIdElement.InnerText = condition.ValueSource.Id.ToString();
        }

        XmlElement comparisonValueElement = ownerDoc.CreateElement( "ComparisonValue" );
        conditionElement.AppendChild( comparisonValueElement );
        if( condition.ValueSource != null )
        {
          comparisonValueElement.InnerText = condition.ComparisonValue.ToString();
        }

        XmlElement comparisonTypeElement = ownerDoc.CreateElement( "ComparisonType" );
        comparisonTypeElement.AppendChild( comparisonValueElement );
        if( condition.ValueSource != null )
        {
          comparisonTypeElement.InnerText =
            ValueComparison.ComparisonAsString( condition.ComparisonType ) ?? "";
        }
      }
      
      return andElement; 
    }

    //-------------------------------------------------------------------------

    // Restore this instance from xml.

    public override XmlElement RestoreFromXml( XmlElement parent )
    {
      // Must call base method.
      parent = base.RestoreFromXml( parent );

      // 'And' processor.
      XmlElement andElement = parent[ "And" ];

      // Condition collection.
      XmlElement conditionCollection = andElement[ "ConditionCollection" ];

      foreach( XmlElement conditionElement in conditionCollection.SelectNodes( "Condition" ) )
      {
        Condition condition = new Condition();

        Circuit circuit =
          Context.Board.Circuits[
            uint.Parse(
              conditionElement[ "ValueSourceId" ].InnerText ) ];
        condition.ValueSource = circuit.Context;

        condition.ComparisonValue = conditionElement[ "ComparisonValue" ].InnerText;

        condition.ComparisonType =
          ValueComparison.ComparisonFromString(
            conditionElement[ "ComparisonType" ].InnerText );
      }

      return andElement;
    }

    //-------------------------------------------------------------------------
  }
}
