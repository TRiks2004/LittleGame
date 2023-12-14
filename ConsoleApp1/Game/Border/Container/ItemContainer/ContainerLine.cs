namespace ConsoleApp1.Game.Border.Container.ItemContainer;

public class ContainerLine
{
    public List<ContainerLinePart> Parts { get; set; }
    
    public Alignment.Alignment AlignmentContainer { get; set; }
    
    public ContainerLine(Alignment.Alignment alignment = Alignment.Alignment.Start)
    {
        Parts = new List<ContainerLinePart>();
        AlignmentContainer = alignment;
    }
    
    public ContainerLine(List<ContainerLinePart> parts, Alignment.Alignment alignment = Alignment.Alignment.Start): this(alignment)
    {
        Parts = parts;
    }
    
    public void Add(ContainerLinePart containerLinePart)
    {
        Parts.Add(containerLinePart);
    }
    
    public int GetTextLength()
    {
        return Parts.Sum(part => part.TextLength);
    }
    
}