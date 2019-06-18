namespace GeneticAlgorithm.Termination
{
    using System;

    public class GenerationCountTermination<TGeneSequence> : ITermination<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        private readonly int _generations;

        public GenerationCountTermination(int generations)
        {
            _generations = generations;
        }

        public bool ShouldTerminate(GeneticAlgorithmSession<TGeneSequence> geneticAlgorithmSession)
        {
            if (geneticAlgorithmSession.GenerationCount >= _generations)
            {
                return true;
            }

            return false;
        }
    }
}
