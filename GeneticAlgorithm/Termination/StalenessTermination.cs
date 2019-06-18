namespace GeneticAlgorithm.Termination
{
    using System;
    using System.Linq;

    public class StalenessTermination<TGeneSequence> : ITermination<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        private readonly int _staleGenerations;
        private int _staleCount;
        private double? _lastBestFitness = 0;

        public StalenessTermination(int staleGenerations)
        {
            _staleGenerations = staleGenerations;
        }

        public bool ShouldTerminate(GeneticAlgorithmSession<TGeneSequence> geneticAlgorithmSession)
        {
            var bestFitness = geneticAlgorithmSession.CurrentPopulation.Max(x => x.FitnessScore);

            if (bestFitness == _lastBestFitness)
            {
                _staleCount++;
            }
            else
            {
                _staleCount = 0;
                _lastBestFitness = bestFitness;
            }

            if (bestFitness > 0 && _staleCount >= _staleGenerations)
            {
                return true;
            }

            return false;
        }
    }
}
