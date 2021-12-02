# ServiceClientExample

This project is a very basic sample to use CastelProxy intercrptors to implmenet an Attirbute that adds the autentication aspect to a ServiceClient

## How it works :
- In order to attach the attribute to each method , the service should be inherited from IServiceInterface that contains a HttpClient.
- Then we should add the ServiceClient to the ServiceCollection using AddServiceClient extension method to create a proxy that uses the intercpetor to find the attached strategy based on the Attribute
