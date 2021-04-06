# twitter-app

The twitter app features 4 projects

1. twitter-app-console
   - Used for showing limited data from the stream, can only start processing of stream. Will need to close console to stop the stream. 
2. twitter-app.tests
   - Used to validate reporting data from the twitter-data library. Uses sample data in artifacts folder for calculations.
3. twitter-app.ui
   -  ASP.NET Core web application, user friendly UI, displays more data regarding information from the twitter stream. Top 10 results, emojis, etc...
   -  Can cancel the stream with cancellation tokens. Updates data on the screen every 3 seconds. 
4. twitter-data
   - Performs the data calculations and handles the streaming of twitter data. 

