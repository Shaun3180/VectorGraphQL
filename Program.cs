/* Created by Shaun Geisert @ Colorado State University on 9/22/2022 */

var vector = new Vector();

//Console.WriteLine();
//Console.WriteLine("---------- Getting user by external id ---------- ");
//var userByExternalId = await vector.GetUserByUniqueId("821161783"); // not sure why they are returning People instead of Person
//Console.WriteLine($"Returned user by external unique id: {userByExternalId.First} {userByExternalId.Last} {userByExternalId.ExternalUniqueId}");

//Console.WriteLine();
//Console.WriteLine("---------- Getting user by user name ---------- ");
//var userByUserName = await vector.GetUserByUniqueId("sgarten"); // not sure why they are returning People instead of Person
//Console.WriteLine($"Returned user by user name: {userByUserName.First} {userByUserName.Last} {userByUserName.Username}");

//Console.WriteLine("---------- Adding New Parent Classification/Position ---------- ");
//var newPosition = await vector.AddPosition("Student Type", "student-type", Guid.Empty);
//Console.WriteLine($"Successfully created parent position: {newPosition.Name}");

//Console.WriteLine();
//Console.WriteLine("---------- Adding New Child Classification/Position ---------- ");
//var newChildPosition = await vector.AddPosition("Returning", "returning", new Guid(newPosition.PositionId));
//Console.WriteLine($"Successfully created child position: {newChildPosition.Name}");

Console.WriteLine();
Console.WriteLine("---------- Getting All Classifications/Positions ---------- ");
var positions = await vector.GetAllPositions();
foreach (var position in positions)
{
    Console.WriteLine($"{position.PositionId}: {position.Name}");
}
return;
Console.WriteLine();
Console.WriteLine("---------- Getting All Locations ---------- ");
var locations = await vector.GetAllLocations();
foreach (var location in locations)
{
    Console.WriteLine($"{location.LocationId}: {location.Name}");
}

Console.WriteLine();
Console.WriteLine("---------- Adding a New User ---------- ");
var newPerson = new VectorStudent
{
    CsuId = "820213885",
    FirstName = "Alicia",
    LastName = "Schueler",
    EName = "aliciasc",
    Email = "Alicia.Rice@colostate.edu"
};
var createdPerson = await vector.AddPerson(newPerson);
Console.WriteLine($"Successfully created new user: {createdPerson.PersonId} {createdPerson.Last}");

//Console.WriteLine();
//Console.WriteLine("---------- Adding a New User w/ Position Id We Just Created ---------- ");
//var newUser = new Person
//{
//    ExternalUniqueId = "123",
//    FirstName = "Julie",
//    LastName = "Pignataro",
//    UserName = "jpignat",
//    PositionId = new Guid(newChildPosition.PositionId)
//};
//var createdPerson = await vector.AddPerson(newUser);
//Console.WriteLine($"Successfully created new user: {createdPerson.PersonId} {createdPerson.First}");

Console.WriteLine();
Console.WriteLine("---------- Adding a New Job to Existing User (using first location from above) ---------- ");
var newJob = await vector.AddJobToPerson(new Guid(createdPerson.PersonId), new Guid(locations[0].LocationId), new Guid(positions[0].PositionId));
Console.WriteLine($"Successfully assigned job to user: {createdPerson.PersonId}");

Console.WriteLine();
Console.WriteLine("---------- Getting First 100 Active Users ---------- ");
var activeUsers = await vector.GetFirst100ActivePeopleAndProgress();
foreach (var person in activeUsers)
{
    Console.WriteLine($"{person.PersonId} {person.Username} {person.First}");
}

Console.WriteLine();
Console.WriteLine("---------- Getting user we just created ---------- ");
var user = await vector.GetUserByGuid(new Guid(createdPerson.PersonId));
Console.WriteLine($"Returned created user: {user.PersonId} {user.Username} {user.First}");

Console.WriteLine();
Console.WriteLine("---------- Updating First Name For That User ---------- ");
var p = new VectorStudent(user);
p.FirstName = p.FirstName + "2";
var updatedUser = await vector.UpdateUser(p);
Console.WriteLine($"Updated first name of user: {user.First} to {p.FirstName}");

Console.WriteLine();
Console.WriteLine("---------- Deactivate the User We Just Updated ---------- ");
var deactivatedUser = await vector.DeactivatePersonById(new Guid(user.PersonId));
Console.WriteLine($"Successfully deactivated: {user.PersonId} {user.Username} {user.First}");