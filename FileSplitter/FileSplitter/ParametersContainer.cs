using System;

namespace Tyshkavets.FileSplitter
{
    internal sealed class ParametersContainer
    {
        public String InputFileName { get; private set; }
        public int NumberOfLinesInOnePartition { get; private set; }
        public String PartitionNamePrefix { get; private set; }

        public ParametersContainer(String inputFileName, int numberOfLinesInOnePartition, String partitionNamePrefix)
        {
            PartitionNamePrefix = partitionNamePrefix;
            NumberOfLinesInOnePartition = numberOfLinesInOnePartition;
            InputFileName = inputFileName;
        }
    }
}
