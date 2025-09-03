using Domain.Common;

namespace Application.Models.Item
{
    public sealed record ItemFieldValueDto(
    Guid FieldId,
    FieldType Type,
    string? StringValue,
    decimal? NumericValue,
    bool? BoolValue
);
}
