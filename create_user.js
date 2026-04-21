db.createUser({ user: "userUV", pwd: "password123", roles: [{ role: 'readWrite', db: 'SensorData' }] });
db.users.insertOne({
    "userName": "elyte",
    "userId": "3a123c17-0093-49e3-bb3a-5bd28cb67496",
    "password": "12345",
    "email": "elyte5star@gmail.com",
    "verify": 0,
    "string": "KingOfTheWest",
    "roles": ["USER", "ADMIN"],
    "isActive": true
});

