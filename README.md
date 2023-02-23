# Widget

This was built using the latest versions of Angular and .NET Core. The architecture pattern chosen for the back-end is a loose Onion Architecture approach. The weather and news data are fetched from public APIs, but the local file/application data is mocked. PrimeNG was the material library used on the client side.

![widget-demo](https://user-images.githubusercontent.com/7450751/220827759-f9a4fa0a-ae18-4086-8d77-418e09ffcd7c.gif)

# API Documentation

Weather

/GET

Query Parameters: q (Search String) 

http://localhost:54366/v1/weather

Local

/GET

Query Parameters: q (Search String) pageSize (Take)

http://localhost:54366/v1/local

News

/GET

Query Parameters: q (City), from (Date), page (Page), pageSize (Take)

http://localhost:54366/v1/news

# Get Started

To start the project, you'll do `dotnet run` targeting the IIS Express Run Configuration

TODOs: 
- Add code coverage
- Implement more cacheing where necessary
- Clean up front-end architecture, components, theming, etc
