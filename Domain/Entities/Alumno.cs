namespace Domain;

public class Alumno
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
	
    public string Foto { get; set; }
}

/*
CREATE TABLE [Empleado](
	[Id] [uniqueidentifier] PRIMARY KEY,
	[Nombre] [nvarchar](128) NOT NULL,
	[Edad] [tinyint] NOT NULL,
	
        --[Foto] [nvarchar](max) NULL,
)
*/