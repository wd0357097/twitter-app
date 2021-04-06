# twitter-app

The twitter app features 4 projects

1. twitter-app-console
   - Used for showing limited data from the stream, can only start processing of stream. Will need to close console to stop the stream. 
   -  Uses DI, (Singleton intance) setup to inject an instance of reporting data 
2. twitter-app.tests
   - Used to validate reporting data from the twitter-data library. Uses sample data in artifacts folder for calculations.
3. twitter-app.ui
   -  ASP.NET Core web application, user friendly UI, displays more data regarding information from the twitter stream. Top 10 results, emojis, etc...
   -  Can cancel the stream with cancellation tokens. Updates data on the screen every 3 seconds. 
   -  Uses DI, (Singleton intance) setup to inject an instance of reporting data 
4. twitter-data
   - Performs the data calculations and handles the streaming of twitter data. 


Data Gathering 

•	Total number of tweets received 

•	Average tweets per hour/minute/second 

•	Top emojis in tweets* (Top 10 in UI component)

•	Percent of tweets that contains emojis 

•	Top hashtags (Top 10 in UI component)

•	Percent of tweets that contain a url 

•	Percent of tweets that contain a photo url (pic.twitter.com or Instagram) 

•	Top domains of urls in tweets (Top 10 in UI component)

