namespace GeneticAlgorithm.Chromosome
{
    using System;
    using GeneticAlgorithm.Exceptions;

    public class Chromosome<TGeneSequence> : ICloneable
        where TGeneSequence : ICloneable
    {
        private double? _fitnessScore;

        public Chromosome(TGeneSequence geneSequence)
        {
            GeneSequence = geneSequence;
        }

        public TGeneSequence GeneSequence { get; }

        public double? FitnessScore
        {
            get => _fitnessScore;
            set
            {
                if (_fitnessScore != null)
                {
                    throw new FitnessScoreAlreadySetException("Fitness score already set");
                }

                _fitnessScore = value;
            }
        }

        public object Clone()
        {
            return new Chromosome<TGeneSequence>((TGeneSequence)GeneSequence.Clone());
        }
    }
}
