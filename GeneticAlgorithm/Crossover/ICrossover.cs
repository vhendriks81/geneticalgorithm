namespace GeneticAlgorithm.Crossover
{
    using System;
    using System.Collections.Generic;
    using GeneticAlgorithm.Chromosome;

    public interface ICrossover<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        ICollection<Chromosome<TGeneSequence>> Crossover(ICollection<Chromosome<TGeneSequence>> selection);
    }
}