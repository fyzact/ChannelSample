using System.Collections.Concurrent;
using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;


namespace ChannelSample
{
    [MemoryDiagnoser]
    [Config(typeof(BenchmarkConfig))]
    public  class ChannelBenchmarkTest
    {
        MyChannel<int> _myChannel=new MyChannel<int>();
        Channel<int> _dotnetChannel=Channel.CreateBounded<int>(2);

        [Benchmark]
        public async Task MyChannel(){
            for (int i = 0; i < 1_000_000; i++)
            {
                var rTask=_myChannel.ReadAsync();
                _myChannel.Write(0);
                await rTask;
            }  
        }

        [Benchmark]
        public async Task DotnetChannel(){
            for (int i = 0; i < 1_000_000; i++)
            {
                var rTask=_dotnetChannel.Reader.ReadAsync();
                await _dotnetChannel.Writer.WriteAsync(0);
                await rTask;
            }  
        }

    }
}
