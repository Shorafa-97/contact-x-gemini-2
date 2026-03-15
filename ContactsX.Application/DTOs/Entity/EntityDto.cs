using System.Collections.Generic;
using ContactsX.Application.DTOs.Shared;


namespace ContactsX.Application.DTOs.Entity;

public record EntityDto(
    Guid Id,
    string NameEn,
    string? NameAr,
    string? EntityType,
    string? Country,
    string? Sector,
    string? RegistrationId,
    Guid? ParentEntityId,
    List<AddressDto>? Addresses,
    List<ContactPointDto>? ContactPoints,
    int ProfileCompleteness,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateEntityDto(
    Guid Id,
    string NameEn,
    string? NameAr,
    string? EntityType,
    string? Country,
    string? Sector,
    string? RegistrationId,
    Guid? ParentEntityId,
    List<AddressDto>? Addresses,
    List<ContactPointDto>? ContactPoints,
    int ProfileCompleteness,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record UpdateEntityDto(
    Guid Id,
    string NameEn,
    string? NameAr,
    string? EntityType,
    string? Country,
    string? Sector,
    string? RegistrationId,
    Guid? ParentEntityId,
    List<AddressDto>? Addresses,
    List<ContactPointDto>? ContactPoints,
    int ProfileCompleteness,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
