/*  CTRADER GURU --> Indicator Template 1.0.8

    Homepage    : https://ctrader.guru/
    Telegram    : https://t.me/ctraderguru
    Twitter     : https://twitter.com/cTraderGURU/
    Facebook    : https://www.facebook.com/ctrader.guru/
    YouTube     : https://www.youtube.com/channel/UCKkgbw09Fifj65W5t5lHeCQ
    GitHub      : https://github.com/ctrader-guru

*/

using cAlgo.API;
using cAlgo.API.Indicators;

namespace cAlgo
{

    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    [Levels(0.3, 0.7)]
    public class DeMarker : Indicator
    {

        #region Identity

        public const string NAME = "DeMarker";

        public const string VERSION = "1.0.2";

        #endregion

        #region Params

        [Parameter(NAME + " " + VERSION, Group = "Identity", DefaultValue = "https://www.google.com/search?q=ctrader+guru+demarker")]
        public string ProductInfo { get; set; }

        [Parameter("Periods", Group = "Params", DefaultValue = 14)]
        public int Periods { get; set; }

        [Parameter("Moving Average", Group = "Params", DefaultValue = MovingAverageType.Simple)]
        public MovingAverageType MAtype { get; set; }

        [Output("DMark", LineColor = "DodgerBlue")]
        public IndicatorDataSeries Result { get; set; }

        #endregion

        #region Property

        private IndicatorDataSeries DemarkerMin;
        private IndicatorDataSeries DemarkerMax;
        private MovingAverage DemarkerMAmin;
        private MovingAverage DemarkerMAmax;

        #endregion

        #region Indicator Events

        protected override void Initialize()
        {

            Print("{0} : {1}", NAME, VERSION);

            DemarkerMin = CreateDataSeries();
            DemarkerMax = CreateDataSeries();
            DemarkerMAmin = Indicators.MovingAverage(DemarkerMin, Periods, MAtype);
            DemarkerMAmax = Indicators.MovingAverage(DemarkerMax, Periods, MAtype);

        }

        public override void Calculate(int index)
        {

            if (Bars.HighPrices[index] > Bars.HighPrices[index - 1])
            {

                DemarkerMax[index] = Bars.HighPrices[index] - Bars.HighPrices[index - 1];

            }
            else
            {

                DemarkerMax[index] = 0;

            }

            if (Bars.LowPrices[index] < Bars.LowPrices[index - 1])
            {

                DemarkerMin[index] = Bars.LowPrices[index - 1] - Bars.LowPrices[index];

            }
            else
            {

                DemarkerMin[index] = 0;

            }

            Result[index] = DemarkerMAmax.Result[index] / (DemarkerMAmax.Result[index] + DemarkerMAmin.Result[index]);

        }

        #endregion

    }

}
