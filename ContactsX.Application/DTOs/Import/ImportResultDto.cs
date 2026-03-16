namespace ContactsX.Application.DTOs.Import;

public record ImportResultDto(int Imported, List<string> Errors);
