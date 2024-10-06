using System;

namespace DapperLab.Domain;

public class User
{
    public Ulid? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? Created { get; set; }
    public double Age { get; set; }
    
}