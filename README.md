# IssueReportPortal


This project aims to demonstrate how to create a message based system, with the idea of an Issue Reporting Portal where users can describe the issue faced, and select is urgency level, if urgent, an email will be sent to the admin, then all issues will be logged.<br/>
<br/>
Using the following Azure services: </br>
  1- Azure Web Apps </br>
  2- ServiceBus </br>
  3- Azure Functions </br>
  4- Cosmos DB </br>
  5- Logic Apps 
  6- Storage Queue </br>

## Solution structure 
<img width="1399" alt="Screenshot 1442-06-22 at 11 02 46 AM" src="https://user-images.githubusercontent.com/50453450/106872181-140e7300-66e4-11eb-865f-447874bbbcde.png">
  
With the idea of an issue reporting portal where users can describe the issue faced, and select is urgently level. 

## Solution flow 

The solution would be hosted on a web app, using its URL users can access it and report the issue they are facing; once the report button is hit, 
the message will be added to our Service Bus queue to increase our solution's reliability and makes sure all problems are handled, 
then it will be sent to Azure function, where it will be processed, if it is urgent it will be added to A Storage Account queue to be hold tempoarily before its sent to an Azure Logic App that will send a warning email to the admin; lastly all issues (urgent or not) will be logged in a CosmosDB.
