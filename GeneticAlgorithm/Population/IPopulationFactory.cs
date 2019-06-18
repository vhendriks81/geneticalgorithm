namespace GeneticAlgorithm.Population
{
    using System;
    using GeneticAlgorithm.Chromosome;

    public interface IPopulationFactory<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        Chromosome<TGeneSequence> Create();
    }
}