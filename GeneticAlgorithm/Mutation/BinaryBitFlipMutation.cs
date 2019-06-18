namespace GeneticAlgorithm.Mutation
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using GeneticAlgorithm.Chromosome;

    public class BinaryBitFlipMutation : IMutation<BitArray>
    {
        private readonly double _mutationProbability;
        private readonly Random _random = new Random();

        public BinaryBitFlipMutation(double mutationProbability)
        {
            _mutationProbability = mutationProbability;
        }

        public void Mutate(ICollection<Chromosome<BitArray>> currentPopulation)
        {
            foreach (var chromosome in currentPopulation)
            {
                // Loop through each gene and possibly mutate that gene depending on the mutation probability.
                for (var geneIndex = 0; geneIndex < chromosome.GeneSequence.Count; geneIndex++)
                {
                    if (_random.NextDouble() <= _mutationProbability)
                    {
                        chromosome.GeneSequence[geneIndex] = !chromosome.GeneSequence[geneIndex];
                    }
                }
            }
        }
    }
}
