namespace GeneticAlgorithm.Crossover
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using GeneticAlgorithm.Chromosome;

    public class BinarySinglePointCrossover : ICrossover<BitArray>
    {
        private readonly Random _random = new Random();

        public ICollection<Chromosome<BitArray>> Crossover(ICollection<Chromosome<BitArray>> selection)
        {
            var resultingPopulation = new List<Chromosome<BitArray>>();

            // We use each parent and let them all create children, hoping that a new, improved chromosome evolves.
            foreach(var parent1 in selection)
            {
                foreach (var parent2 in selection)
                {
                    // Let's not do this... In this algorithm it would just create 2 identical children.
                    if (parent1 != parent2)
                    {
                        resultingPopulation.AddRange(CreateChildren(parent1, parent2));
                    }
                }
            }
            
            return resultingPopulation;
        }

        private ICollection<Chromosome<BitArray>> CreateChildren(Chromosome<BitArray> parent1, Chromosome<BitArray> parent2)
        {
            // Determine a random point within the gene sequence
            var crossOverPoint = _random.Next(0, parent1.GeneSequence.Count);

            // Create two new gene sequences. 1 for each child
            var child1GeneSequence = new BitArray(parent1.GeneSequence.Count);
            var child2GeneSequence = new BitArray(parent1.GeneSequence.Count);
        
            // Set the genes on the left side of the crossover point
            for (var geneIndex = 0; geneIndex < crossOverPoint; geneIndex++)
            {
                // Child #1 gets the left part of parent #1
                child1GeneSequence[geneIndex] = parent1.GeneSequence[geneIndex];

                // Child #2 gets the left part of parent #2
                child2GeneSequence[geneIndex] = parent2.GeneSequence[geneIndex];
            }

            // Set the genes on the right side of the crossover point
            for (var geneIndex = crossOverPoint; geneIndex < parent1.GeneSequence.Length; geneIndex++)
            {
                // Child #1 gets the right part of parent #2
                child1GeneSequence[geneIndex] = parent2.GeneSequence[geneIndex];
                
                // Child #2 gets the right part of parent #1
                child2GeneSequence[geneIndex] = parent1.GeneSequence[geneIndex];
            }

            // Create the children
            var child1 = new Chromosome<BitArray>(child1GeneSequence);
            var child2 = new Chromosome<BitArray>(child2GeneSequence);

            return new[] { child1, child2 };
        }
    }
}
