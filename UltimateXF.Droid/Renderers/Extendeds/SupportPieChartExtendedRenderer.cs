﻿using System.ComponentModel;
using System.Linq;
using Android.Content;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using UltimateXF.Droid.Renderers.Extendeds;
using UltimateXF.Widget.Charts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SupportPieChartExtended), typeof(SupportPieChartExtendedRenderer))]
namespace UltimateXF.Droid.Renderers.Extendeds
{
    public class SupportPieChartExtendedRenderer : SupportPieRadarChartBaseExtendedRenderer<SupportPieChartExtended, PieChart>
    {
        public SupportPieChartExtendedRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(nameof(SupportPieChartExtended.ChartData)))
            {
                OnInitializeChartData();
            }
        }

        protected override void OnInitializeOriginalChartSettings()
        {
            base.OnInitializeOriginalChartSettings();
            if (SupportChartView != null && OriginalChartView != null)
            {
                /*
                 * Properties could not set
                 * DrawRoundedSlices
                 * AbsoluteAngles
                 * DrawAngles
                 */
                OriginalChartView.SetNoDataText("无可用数据");

                if (SupportChartView.DrawEntryLabels.HasValue)
                    OriginalChartView.SetDrawEntryLabels(SupportChartView.DrawEntryLabels.Value);

                if (SupportChartView.DrawHole.HasValue)
                    OriginalChartView.DrawHoleEnabled = SupportChartView.DrawHole.Value;

                if (SupportChartView.DrawSlicesUnderHole.HasValue)
                    OriginalChartView.SetDrawSlicesUnderHole(SupportChartView.DrawSlicesUnderHole.Value);

                if (SupportChartView.UsePercentValues.HasValue)
                    OriginalChartView.SetUsePercentValues(SupportChartView.UsePercentValues.Value);

                OriginalChartView.CenterText = (SupportChartView.CenterText);

                if (SupportChartView.HoleRadiusPercent.HasValue)
                    OriginalChartView.HoleRadius = (SupportChartView.HoleRadiusPercent.Value);

                if (SupportChartView.TransparentCircleRadiusPercent.HasValue)
                    OriginalChartView.TransparentCircleRadius = (SupportChartView.TransparentCircleRadiusPercent.Value);

                if (SupportChartView.CenterTextRadiusPercent.HasValue)
                    OriginalChartView.CenterTextRadiusPercent = (SupportChartView.CenterTextRadiusPercent.Value);

                if (SupportChartView.MaxAngle.HasValue)
                    OriginalChartView.MaxAngle = (SupportChartView.MaxAngle.Value);

                if (SupportChartView.DrawCenterText.HasValue)
                    OriginalChartView.SetDrawCenterText(SupportChartView.DrawCenterText.Value);
            }
        }

        protected override void OnInitializeChartData()
        {
            base.OnInitializeChartData();
            if (OriginalChartView != null && SupportChartView != null && SupportChartView.ChartData != null)
            {
                var dataSupport = SupportChartView.ChartData;
                var dataSetSource = dataSupport.DataSets.FirstOrDefault();

                if(dataSetSource!=null)
                {
                    var entryOriginal = dataSetSource.IF_GetValues().Select(item => new PieEntry(item.GetPercent(), item.GetText()));
                    PieDataSet dataSet = new PieDataSet(entryOriginal.ToArray(), dataSetSource.IF_GetLabel());
                    OnIntializeDataSet(dataSetSource,dataSet);
                    var data = new PieData(dataSet);
                    OriginalChartView.Data = data;
                }
                OriginalChartView.Invalidate();
            }
        }

        private void OnIntializeDataSet(Widget.Charts.Models.PieChart.IPieDataSet source, PieDataSet original)
        {
            /*
             * Properies could not net
             * IF_GetUsingSliceColorAsValueLineColor
             */
            Export.OnSettingsBaseDataSet(source, original);

            if (source.IF_GetSliceSpace().HasValue)
                original.SliceSpace = (source.IF_GetSliceSpace().Value);

            if (source.IF_GetAutomaticallyDisableSliceSpacing().HasValue)
                original.SetAutomaticallyDisableSliceSpacing(source.IF_GetAutomaticallyDisableSliceSpacing().Value);

            if (source.IF_GetShift().HasValue)
                original.SelectionShift = (source.IF_GetShift().Value);

            if (source.IF_GetValueLineColor().HasValue)
                original.ValueLineColor = (source.IF_GetValueLineColor().Value.ToAndroid());
            
            if (source.IF_GetValueLineWidth().HasValue)
                original.ValueLineWidth = (source.IF_GetValueLineWidth().Value);

            if (source.IF_GetValueLinePart1OffsetPercentage().HasValue)
                original.ValueLinePart1OffsetPercentage = (source.IF_GetValueLinePart1OffsetPercentage().Value);

            if (source.IF_GetValueLinePart1Length().HasValue)
                original.ValueLinePart1Length = (source.IF_GetValueLinePart1Length().Value);

            if (source.IF_GetValueLinePart2Length().HasValue)
                original.ValueLinePart2Length = (source.IF_GetValueLinePart2Length().Value);

            if (source.IF_GetValueLineVariableLength().HasValue)
                original.SetValueLineVariableLength(source.IF_GetValueLineVariableLength().Value);

            if (source.IF_GetXValuePosition().HasValue)
                original.XValuePosition = source.IF_GetXValuePosition().Value == Widget.Charts.Models.PieChart.ValuePosition.INSIDE_SLICE ? PieDataSet.ValuePosition.InsideSlice : PieDataSet.ValuePosition.OutsideSlice;

            if (source.IF_GetYValuePosition().HasValue)
                original.YValuePosition = source.IF_GetYValuePosition().Value == Widget.Charts.Models.PieChart.ValuePosition.INSIDE_SLICE ? PieDataSet.ValuePosition.InsideSlice : PieDataSet.ValuePosition.OutsideSlice;
        }
    }
}