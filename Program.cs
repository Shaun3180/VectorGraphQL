/* Questions for Vector:
 * 
    * What's the difference between a position/classification and a location?
    * How do you disassociate a position from a user?  
    * How many distinct labels can you apply to one user?  Is there a limit?
    * You can’t update a position or location via your API for a person as far as I can tell
    * How do you activate someone via the API?
 */

var vector = new Vector();

Console.WriteLine("---------- Adding New Parent Classification/Position ---------- ");
var newPosition = await vector.AddPosition("Campus", "campus", Guid.Empty);
Console.WriteLine("Successfully created parent position: " + newPosition.PositionId);

Console.WriteLine();
Console.WriteLine("---------- Adding New Child Classification/Position ---------- ");
var newChildPosition = await vector.AddPosition("Online", "online", new Guid(newPosition.PositionId));
Console.WriteLine("Successfully created child position: " + newPosition.PositionId);

Console.WriteLine();
Console.WriteLine("---------- Getting All Classifications/Positions ---------- ");
var positions = await vector.GetAllPositions();
foreach (var position in positions)
{
    Console.WriteLine(position.PositionId + ": " + position.Name);
}

Console.WriteLine();
Console.WriteLine("---------- Adding a New User w/ Position Id We Just Created ---------- ");
var newUser = new Person {
    ExternalUniqueId = "820182056",
    FirstName = "Joy",
    LastName = "Akey",
    UserName = "jakey",
    PositionId = new Guid(newChildPosition.PositionId)
};
var createdUser = await vector.AddUser(newUser);

Console.WriteLine();
Console.WriteLine("---------- Getting First 100 Active Users ---------- ");
var activeUsers = await vector.GetFirst100ActivePeopleAndProgress();
foreach (var person in activeUsers)
{
    Console.WriteLine(person.PersonId + ": " + person.Username + " " + person.First + " " + person.Last);
}

Console.WriteLine();
Console.WriteLine("---------- Getting First User From Active User List ---------- ");
var user = await vector.GetUserByGuid(new Guid(activeUsers[0].PersonId));
Console.WriteLine(user.PersonId + ": " + user.Username + " " + user.First + " " + user.Last);


Console.WriteLine();
Console.WriteLine("---------- Updating First Name For That User ---------- ");
var p = new Person(user);
p.FirstName = p.FirstName + "2";
var updatedUser = await vector.UpdateUser(p);
Console.WriteLine(user.PersonId + ": " + user.Username + " " + user.First + " " + user.Last);

Console.WriteLine();
Console.WriteLine("---------- Deactivate the User We Just Updated ---------- ");
var deactivatedUser = await vector.DeactivatePersonById(new Guid(user.PersonId));
Console.WriteLine("Successfully deactivated: " + user.PersonId);
return;