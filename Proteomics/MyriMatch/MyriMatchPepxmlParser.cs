﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchPepXmlParser : PeptideProphetXmlParser
  {
    public MyriMatchPepXmlParser() { }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.MyriMatch; }
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    protected override void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      base.ParseScore(sph, searchHit);

      var scores = searchHit.FindDescendants("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("mvh"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("mzFidelity"))
        {
          sph.SpScore = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    #endregion
  }
}
