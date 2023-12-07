# Databaser_Labb3_V2
Requires a appsettings.json file to connect to your database.
I have configed the DBContext to search after the connection string for EdugradeHighSchool.
Create a appsettings.json file in your root directory that looks like this but make sure to insert your own connectiong string
```json
{
  "ConnectionStrings": {
    "EdugradeHighSchool": "YOUR_CONNECTION_STRING_HERE"
  }
}
```
