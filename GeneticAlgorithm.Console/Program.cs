namespace GeneticAlgorithm.Console
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using GeneticAlgorithm.Console.Evaluation;
    using GeneticAlgorithm.Console.Models;
    using GeneticAlgorithm.Crossover;
    using GeneticAlgorithm.Mutation;
    using GeneticAlgorithm.Population;
    using GeneticAlgorithm.Selection;
    using GeneticAlgorithm.Termination;

    static class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            // Generate a list of products with random weights and prices
            var products = new List<Product>();
            for (var i = 0; i < 50; i++)
            {
                products.Add(new Product(random.Next(100) + 1, random.Next(100) + 1));
            }

            var maxWeight = 200;

            var populationFactory = new BasicBinaryPopulationFactory(products.Count, false);

            // Terminate if the highest fitness score remains the same after X generations
            var termination = new StalenessTermination<BitArray>(5);

            var fitnessEvaluator = new KnapsackFitnessEvaluator(maxWeight, products.AsReadOnly());
            var crossover = new BinarySinglePointCrossover();

            // Preserve the best chromosome for the next generation
            var preservationSelection = new BestFitnessSelection<BitArray>(1);

            // Use the top 30 chromosomes to create children for the next generation
            var parentSelection = new BestFitnessSelection<BitArray>(30);

            // Each gene has a 1% chance to mutate. Each mutation is a bit flip
            var mutation = new BinaryBitFlipMutation(0.01);

            var geneticAlgorithm = new GeneticAlgorithm<BitArray>(populationFactory, termination, fitnessEvaluator, preservationSelection, parentSelection, crossover, mutation, 200);

            // Let the "breeding" begin
            var geneticAlgorithmResult = geneticAlgorithm.Start();
                
            var bestChromosome = geneticAlgorithmResult.Selection.First();

            Console.WriteLine();
            Console.WriteLine($"Max score using genetic algorithm: {bestChromosome.FitnessScore}");
            Console.WriteLine($"Max score using dynamic programming: {CalculateMaximumValue(products.ToArray(), maxWeight)}");
        }


        /// <summary>
        /// Algorithm to solve the 0-1 knapsack problem. Source: https://stackoverflow.com/questions/50393489/knapsack-c-sharp-implementation-task
        /// </summary>
        public static int CalculateMaximumValue(Product[] items, int capacity)
        {
            var matrix = new int[items.Length + 1, capacity + 1];
            for (var itemIndex = 0; itemIndex <= items.Length; itemIndex++)
            {
                // This adjusts the itemIndex to be 1 based instead of 0 based
                // and in this case 0 is the initial state before an item is
                // considered for the knapsack.
                var currentItem = itemIndex == 0 ? null : items[itemIndex - 1];
                for (var currentCapacity = 0; currentCapacity <= capacity; currentCapacity++)
                {
                    // Set the first row and column of the matrix to all zeros
                    // This is the state before any items are added and when the
                    // potential capacity is zero the value would also be zero.
                    if (currentItem == null || currentCapacity == 0)
                    {
                        matrix[itemIndex, currentCapacity] = 0;
                    }
                    // If the current items weight is less than the current capacity
                    // then we should see if adding this item to the knapsack 
                    // results in a greater value than what was determined for
                    // the previous item at this potential capacity.
                    else if (currentItem.Weight <= currentCapacity)
                    {
                        matrix[itemIndex, currentCapacity] = Math.Max(currentItem.Price + matrix[itemIndex - 1, currentCapacity - currentItem.Weight], matrix[itemIndex - 1, currentCapacity]);
                    }
                    // current item will not fit so just set the value to the 
                    // what it was after handling the previous item.
                    else
                    {
                        matrix[itemIndex, currentCapacity] = matrix[itemIndex - 1, currentCapacity];
                    }
                }
            }

            // The solution should be the value determined after considering all
            // items at all the intermediate potential capacities.
            return matrix[items.Length, capacity];
        }
    }
}
