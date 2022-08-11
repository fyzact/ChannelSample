using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace ChannelSample
{
    internal class BenchmarkConfig: ManualConfig
    {
        public BenchmarkConfig()
        {
            AddColumn(StatisticColumn.AllStatistics);
            AddAnalyser(new BaselineCustomAnalyzer());
        }
    }
}
