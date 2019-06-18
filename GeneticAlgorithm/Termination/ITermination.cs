namespace GeneticAlgorithm.Termination
{
    using System;

    public interface ITermination<TGeneSequence>
        where TGeneSequence : ICloneable
    {
        bool ShouldTerminate(GeneticAlgorithmSession<TGeneSequence> geneticAlgorithmSession);
    }
}
