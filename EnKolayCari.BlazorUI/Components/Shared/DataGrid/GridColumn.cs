using System.Linq.Expressions;
using LumexUI.Common;

namespace EnKolayCari.BlazorUI.Components.Shared.DataGrid;

/// <summary>Describes one column of an <see cref="AppDataGrid{TItem}"/>: how it is displayed, sorted, filtered and exported.</summary>
public sealed class GridColumn<TItem>
{
    public required string Title { get; init; }

    public required Expression<Func<TItem, string?>> Property { get; init; }

    public Align Align { get; init; } = Align.Start;

    public bool Sortable { get; init; } = true;

    public bool Filterable { get; init; } = true;
}
