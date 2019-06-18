namespace GeneticAlgorithm.Mutation
{
    using System;
    using System.Collections.Generic;
    using GeneticAlgorithm.Chromosome;

    public interface IMutation<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        void Mutate(ICollection<Chromosome<TGeneSequence>> currentPopulation);
    }
}