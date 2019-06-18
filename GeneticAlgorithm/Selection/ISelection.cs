namespace GeneticAlgorithm.Selection
{
    using System;
    using System.Collections.Generic;
    using GeneticAlgorithm.Chromosome;

    public interface ISelection<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        ICollection<Chromosome<TGeneSequence>> Select(ICollection<Chromosome<TGeneSequence>> population);
    }
}