using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Crossover;
using GeneticAlgorithm.Evaluation;
using GeneticAlgorithm.Mutation;
using GeneticAlgorithm.Population;
using GeneticAlgorithm.Selection;
using GeneticAlgorithm.Termination;
using Xunit;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class GeneticAlgorithmTests
    {
        [Fact]
        public void Start_ShouldReturnBestChromosome()
        {
            // Arrange
            var populationFactory = new BasicBinaryPopulationFactory(10, false);
            var termination = new GenerationCountTermination<BitArray>(1);
            var fitnessEvaluator = new TestFitnessEvaluator();
            var preservationSelection = new BestFitnessSelection<BitArray>(1);
            var parentSelection = new BestFitnessSelection<BitArray>(2);
            var crossover = new BinarySinglePointCrossover();
            var mutation = new BinaryBitFlipMutation(0.01);
            var geneticAlgorithm = new GeneticAlgorithm<BitArray>(populationFactory, termination, fitnessEvaluator, preservationSelection, parentSelection, crossover, mutation, 10);

            // Act
            var result = geneticAlgorithm.Start();

            // Assert
            result.Should().NotBeNull();
            result.Selection.Should().NotBeEmpty();
            result.Selection.First().GeneSequence.Count.Should().Be(10);
        }

        private class TestFitnessEvaluator : IFitnessEvaluator<BitArray>
        {
            public double Evaluate(Chromosome<BitArray> chromosome)
            {
                return chromosome.GeneSequence.Cast<bool>().Count(bit => bit);
            }
        }
    }
}
