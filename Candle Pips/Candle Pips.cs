using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cAlgo.API;
using cAlgo.API.Collections;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;

namespace cAlgo
{
    [Indicator(AccessRights = AccessRights.None)]
    public class CandlePips : Indicator
    {
        [Parameter("Periods", DefaultValue = "1000")]
        public int Periods { get; set; }

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }

        List<double> Pips { get; set; } = new();

        protected override void Initialize()
        {

        }

        public override void Calculate(int index)
        {
            double candleSize = Math.Abs(Bars.LowPrices[index] - Bars.HighPrices[index]);
            double candleSizePips = Math.Round(candleSize / Symbol.PipSize, 3);

            Pips.Add(candleSizePips);

            var avarage = Math.Round(Pips.Count > Periods ? Pips.Average(): 0, 3);

            if(candleSizePips > avarage)
                Chart.DrawText(index.ToString(), candleSizePips.ToString(), index, Bars.HighPrices[index], Color.Red);

            Chart.DrawStaticText("AVARAGE2", "PIPS AVARAGE2: " + avarage.ToString(), VerticalAlignment.Top, HorizontalAlignment.Left, Color.White);

            //ChartObjects.DrawText("AVARAGE", "PIPS AVARAGE: " + avarage.ToString(), StaticPosition.TopLeft, Colors.White);
            //Chart.DrawText(index.ToString(), candleSizePips.ToString(), index, Bars.HighPrices[index], Color.Red);
        }
    }
}