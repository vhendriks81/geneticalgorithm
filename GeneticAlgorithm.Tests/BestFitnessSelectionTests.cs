using System.Collections.Generic;
using System.Collections;
using GeneticAlgorithm.Chromosome;
using GeneticAlgorithm.Selection;
using Xunit;
using FluentAssertions;

namespace GeneticAlgorithm.Tests
{
    public class BestFitnessSelectionTests
    {
        [Fact]
        public void Select_ShouldReturnTopChromosomes()
        {
            // Arrange
            var selectionCount = 2;
            var bestFitnessSelection = new BestFitnessSelection<BitArray>(selectionCount);
            var population = new List<Chromosome<BitArray>>
            {
                CreateChromosome(0.5),
                CreateChromosome(0.8),
                CreateChromosome(0.3),
                CreateChromosome(0.9)
            };

            // Act
            var selectedChromosomes = bestFitnessSelection.Select(population);

            // Assert
            selectedChromosomes.Should().HaveCount(selectionCount);
            selectedChromosomes.Should().Contain(population[1]);
            selectedChromosomes.Should().Contain(population[3]);
        }

        private Chromosome<BitArray> CreateChromosome(double fitnessScore)
        {
            var chromosome = new Chromosome<BitArray>(new BitArray(1));
            chromosome.FitnessScore = fitnessScore;
            return chromosome;
        }
    }
}
