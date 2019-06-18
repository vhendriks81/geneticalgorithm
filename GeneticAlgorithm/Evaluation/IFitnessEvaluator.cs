namespace GeneticAlgorithm.Evaluation
{
    using System;
    using GeneticAlgorithm.Chromosome;

    public interface IFitnessEvaluator<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        double Evaluate(Chromosome<TGeneSequence> chromosome);
    }
}
