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


# IF YOU ARE USING WINDOWS 11
## To get the best experience viewing the database with this application
You will have to go to Windows > Settings > System > For Developers > Terminal and change it to Windows Console Host
This is the only working solution I found as of now to be able to change the console window for the user to be able to 
display the information correctly on the screen! Hopefully I will find a different workaround in the near future, but this
is the only way I know of now.
