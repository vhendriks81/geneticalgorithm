namespace GeneticAlgorithm.Console.Evaluation
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using GeneticAlgorithm.Chromosome;
    using GeneticAlgorithm.Console.Models;
    using GeneticAlgorithm.Evaluation;

    public class KnapsackFitnessEvaluator : IFitnessEvaluator<BitArray>
    {
        private readonly int _maxWeight;
        private readonly ReadOnlyCollection<Product> _products;

        public KnapsackFitnessEvaluator(int maxWeight, ReadOnlyCollection<Product> products)
        {
            _maxWeight = maxWeight;
            _products = products;
        }

        public double Evaluate(Chromosome<BitArray> chromosome)
        {
            var score = 0;
            var weight = 0;

            // Loop through all the bits and if a bit is set to 1 that means that product should be included.
            for (var geneIndex = 0; geneIndex < chromosome.GeneSequence.Count; geneIndex++)
            {
                if (chromosome.GeneSequence[geneIndex])
                {
                    var product = _products[geneIndex];

                    score += product.Price;
                    weight += product.Weight;

                    // If the weight exceeds the maximum weight, the fitness score is 0
                    if (weight > _maxWeight)
                    {
                        return 0;
                    }
                }
            }

            return score;
        }
    }
}
