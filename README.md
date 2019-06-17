# HotConexionDB
 It is a library written in C # that allows manipulating interactions with databases more easily.


# How use it

####  Basic use

```sql
CREATE TABLE Heroes(
	HeroId int,
	HeroName varchar(64),
	AlterEgo varchar(125),
	UniverseId int
)
```

```csharp
public class Hero
{
	public int HeroId { get; set; }
	public string HeroName { get; set; }
	public string AlterEgo { get; set; }
	public int UniverseId { get; set; }
}

Conexion conexion = new Conexion(ConexionType.MSSQLServer, "server", "database", "username", "password");

var result = conexion.ExecuteWithResults<Hero>("SELECT TOP 5 * FROM Heroes");

foreach (var hero in result.Data)
{
	Console.WriteLine(hero.HeroName);
}
```
This returns an object of type **Result** where the resultset is stored in the **Data** property of the object.


####  Parameters

```csharp
ConexionParameters parameters = new ConexionParameters();
parameters.Add("@HeroId", ConexionDbType.Int, 1);

var result = conexion.ExecuteWithResults<Hero>("GetHeroById", parameters);

foreach (var hero in result.Data)
{
	Console.WriteLine(hero.HeroName);
}
```


####  DataSet & DataTable
```csharp
ConexionParameters parameters = new ConexionParameters();
parameters.Add("@HeroId", ConexionDbType.Int, 1); 

var result = conexion.ExecuteWithResults<Hero>("GetHeroById", parameters, out DataSet ds);

var result2 = conexion.ExecuteWithResults<Hero>("GetHeroById", parameters, out DataTable dt);
```


####  Read by row
```csharp
ConexionParameters parameters = new ConexionParameters();
parameters.Add("@Name", ConexionDbType.VarChar, "bat");

conexion.ExecuteWithResults("GetHeroesByName",
			    parameters,
			    row =>
			    {
			        Console.WriteLine("{0} \t {1}", 
				row["HeroId"].ToInt32(), 
				row["HeroName"].ToString());                      
			    });
```


####  Mapping column name

```sql
CREATE TABLE Heroes(
	HeroId int,
	HeroName varchar(64),
	AlterEgo varchar(125),
	UniverseId int
)
```
If you want mapping a column name in the object class you can use the attributte **ConexionColumn**.
```csharp
public class Hero
{
	[ConexionColumn("HeroId")]
	public int Id { get; set; }
	
	[ConexionColumn("HeroName")]
	public string Name { get; set; }
	
	public string AlterEgo { get; set; }
	
	public int UniverseId { get; set; }
}

ConexionParameters parameters = new ConexionParameters();
parameters.Add("@HeroId", ConexionDbType.Int, 1);

var result = conexion.ExecuteWithResults<Hero>("GetHeroById", parameters);

foreach (var hero in result.Data)
{
	Console.WriteLine(hero.Name);
}
```

####  Multiples recordsets

```csharp
conexion.RecordsetsExecute(@"SELECT TOP 10 * FROM Heroes; 
SELECT TOP 5 * FROM Villains");

var heroes = conexion.RecordsetsResults<Hero>();
var villains = conexion.RecordsetsResults<Villain>();

foreach (var hero in heroes.Data)
{
	Console.WriteLine(hero.Name);
}

foreach (var villain in villains.Data)
{
	Console.WriteLine(villain.Name);
}
```

**Try it, enjoy it and have fun :smile:**
