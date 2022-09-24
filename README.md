# VectorGraphQL

The following code utilizes Strawberry Shake (as a C# console app:  https://chillicream.com/docs/strawberryshake/get-started/console) in order to quickly create a strongly-typed means of working with the Vector Solutions LMS: https://www.vectorsolutions.com/solutions/vector-lms/

Even if you're not in a C# environment, you may wish to reference the Queries folder for GraphQL examples that could be utilized in a different environment.

More examples to come, but for now, please see Program.cs for use cases for the code.

## Example Output After Running Program.cs

![Example Output After Running Program.cs](https://github.com/Shaun3180/VectorGraphQL/blob/master/example-output.png)

## Issues

 - I see no way of programmatically activating a user (after they are deactivated)

## GraphQL Schema

    schema {
    query: QueryRoot
    mutation: MutationRoot
    }
    
    
    type QueryRoot {
    Location(locationId: ID!): Location
    Locations(
    code: String
    name: String
    parentId: ID
    after: ID
    before: ID
    first: Int
    last: Int
    ): PagedLocation
    Completions(
    locationId: ID
    endDate: DateTime!
    positionId: ID
    startDate: DateTime!
    after: ID
    before: ID
    first: Int
    last: Int
    ): PagedProgress
    CourseInfo(courseInfoId: ID!): CourseInfo
    Job(jobId: ID!): Job
    Progress(progressId: ID!): Progress
    People(
    locationId: ID
    positionId: ID
    active: Boolean
    after: ID
    before: ID
    first: Int
    last: Int
    ): PagedPerson
    Person(personId: ID!): Person
    Position(positionId: ID!): Position
    Positions(
    code: String
    name: String
    parentId: ID
    after: ID
    before: ID
    first: Int
    last: Int
    ): PagedPosition
    }
    
    
    type MutationRoot {
    Location(locationId: ID!): LocationMutation
    Position(positionId: ID!): PositionMutation
    Person(personId: ID!): PersonMutation
    Job(jobId: ID!): JobMutation
    addLocation(code: String, name: String!, parentId: ID): Location
    addPosition(code: String, name: String!, parentId: ID): Position
    addPerson(
    address1: String
    address2: String
    address3: String
    beginDate: String
    locationId: ID
    city: String
    externalUniqueId: String
    email: String
    first: String!
    middle: String
    last: String!
    password: String
    phone: String
    positionId: ID
    postalCode: String
    state: String
    username: String!
    ): Person
    }
    
    
    type Person {
    address1: String
    address2: String
    address3: String
    city: String
    country: String
    email: String
    externalUniqueId: String
    first: String
    jobs: [Job]
    last: String
    middle: String
    progress: [Progress]
    personId: ID!
    phone: String
    postalCode: String
    state: String
    username: String
    }
    
    type PersonMutation {
    update(
    address1: String
    address2: String
    address3: String
    city: String
    country: String
    email: String
    first: String
    last: String
    phone: String
    postalCode: String
    state: String
    username: String
    ): Person
    changePassword(password: String!): Person
    deactivate: Person
    addJob(
    locationId: ID!
    positionId: ID!
    title: String
    beginDate: Date
    endDate: Date
    ): Job
    }
    
    type JobMutation {
    update(beginDate: Date, endDate: Date, title: String): Job
    deactivate: Job
    }
    
    type Job {
    beginDate: Date
    location: Location!
    endDate: Date
    jobId: ID!
    person: Person!
    position: Position!
    title: String
    }
    
    type Location {
    locationId: ID!
    children: [Location]
    code: String
    name: String
    parent: Location
    }
    
    type LocationMutation {
    remove: Location
    update(code: String, name: String): Location
    }
    
    type Position {
    children: [Position]
    code: String
    name: String
    parent: Position
    positionId: ID!
    }
    
    type PositionMutation {
    remove: Position
    update(code: String, name: String): Position
    }
    
    
    type Progress {
    completed: Boolean
    completeTime: DateTime
    courseInfo: CourseInfo!
    progressId: ID!
    maxQuizScore: Float
    person: Person!
    }
    
    type CourseInfo {
    courseInfoId: ID!
    title: String
    }
    
    
    type PageInfo {
    count: Int
    endCursor: ID
    hasNextPage: Boolean!
    hasPreviousPage: Boolean!
    startCursor: ID
    totalCount: Int
    }
    
    type PagedLocation {
    nodes: [Location]
    pageInfo: PageInfo
    }
    
    type PagedProgress {
    nodes: [Progress]
    pageInfo: PageInfo
    }
    
    type PagedPerson {
    nodes: [Person]
    pageinfo: PageInfo
    }
    
    type PagedPosition {
    nodes: [Position]
    pageInfo: PageInfo
    }
