using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public static class DataflowBlockExtensions {
        public static void CompleteWhenAll(this IDataflowBlock targetBlock, params IDataflowBlock[] sourceBlocks) {
            if (sourceBlocks.Length == 0) {
                targetBlock.Complete();
                return;
            }

            Task.Factory.ContinueWhenAll(
                sourceBlocks.Select(b => b.Completion).ToArray(),
                tasks =>
                {
                    var exceptions = tasks.Where(t => t.IsFaulted).Select(t => t.Exception).ToList();
                    if (exceptions.Count > 0) {
                        targetBlock.Fault(new AggregateException(exceptions));
                    }
                    else {
                        targetBlock.Complete();
                    }
                }
            );
        }
    }
}