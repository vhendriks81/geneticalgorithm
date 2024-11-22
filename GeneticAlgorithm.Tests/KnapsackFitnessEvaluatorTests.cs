using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Console.Evaluation;
using GeneticAlgorithm.Console.Models;
using Xunit;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class KnapsackFitnessEvaluatorTests
    {
        [Fact]
        public void Evaluate_ShouldReturnCorrectFitnessScore_WhenWeightIsWithinLimit()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(10, 60),
                new Product(20, 100),
                new Product(30, 120)
            };
            var maxWeight = 50;
            var evaluator = new KnapsackFitnessEvaluator(maxWeight, products.AsReadOnly());
            var chromosome = new Chromosome<BitArray>(new BitArray(new[] { true, true, false }));

            // Act
            var fitnessScore = evaluator.Evaluate(chromosome);

            // Assert
            fitnessScore.Should().Be(160);
        }

        [Fact]
        public void Evaluate_ShouldReturnZeroFitnessScore_WhenWeightExceedsLimit()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(10, 60),
                new Product(20, 100),
                new Product(30, 120)
            };
            var maxWeight = 25;
            var evaluator = new KnapsackFitnessEvaluator(maxWeight, products.AsReadOnly());
            var chromosome = new Chromosome<BitArray>(new BitArray(new[] { true, true, false }));

            // Act
            var fitnessScore = evaluator.Evaluate(chromosome);

            // Assert
            fitnessScore.Should().Be(0);
        }

        [Fact]
        public void Evaluate_ShouldReturnZeroFitnessScore_WhenNoProductsSelected()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(10, 60),
                new Product(20, 100),
                new Product(30, 120)
            };
            var maxWeight = 50;
            var evaluator = new KnapsackFitnessEvaluator(maxWeight, products.AsReadOnly());
            var chromosome = new Chromosome<BitArray>(new BitArray(new[] { false, false, false }));

            // Act
            var fitnessScore = evaluator.Evaluate(chromosome);

            // Assert
            fitnessScore.Should().Be(0);
        }
    }
}
