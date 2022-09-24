

using VectorGraphQL;

internal class VectorPosition
{
    public Guid PositionId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public IReadOnlyList<IGetAllPositions_Positions_Nodes_Children> Children { get; set; }

    public VectorPosition(IGetAllPositions_Positions_Nodes position)
    {
        this.PositionId = new Guid(position.PositionId);
        this.Name = position.Name;
        this.Code = position.Code;
        this.Children = position.Children;
    }
}

internal class VectorPositions
{
    public VectorPosition Parent { get; set; }
    public VectorPosition Child { get; set; }
}

