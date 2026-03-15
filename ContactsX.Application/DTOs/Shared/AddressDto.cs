namespace ContactsX.Application.DTOs.Shared;

public record AddressDto(string? Type, string? Value, bool IsCurrent = true);
