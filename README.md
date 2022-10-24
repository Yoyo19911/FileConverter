#FileConverter

The project consists of two applications: the website (frontend) and the API (backend).
On the website, you can convert html files to pdf files. The site calls the "html-to-pdf" API method, which returns the body of the pdf file for the passed html file body. The site converts this body into downloadable pdf file.

##Frontend
The site is the simplest html page and is located in FileConverter.Site.
To run the site properly, you need to change the value of the apiUrl variable at the beginning of the js script located at the end of the body tag.

##Backend
The project is made in a simplified Clean architecture. The chosen structure of the project allows it to expand easily. If a significant expansion of the functionality is expected, the API can be rewritten, for example, on a WebAPI with controllers. If domain entities appear, the Domain project can be added. Services, extension classes, custom exceptions and other elements can also be added on any of the layers without changing the current structure.
All projects are placed in the src folder. If tests are to be added, they should be located in the tests folder located at the same level as src.

##API
The API provides only 1 method that performs useful work, so the API layer is implemented using the Minimal API. It is assumed that the API is public and does not require authentication, authorization and restrictions on sources.

##Application
The Application layer contains the business logic and interfaces implemented by the Infrastructure project. This will preserve the logic of the program when replacing the client part (API, Desktop, website, mobile application) and/or infrastructure (replacing the database, file storage interface, etc).

##Infrastructure
The Application layer contains implementations of interaction with third-party systems that can be replaced without changing the interface: operating system, database, third-party APIs, etc.
The HtmlToPdfConverter service internally contains two "infrastructural" elements: creation of the temporary html file and its html to pdf conversion using the PuppeteerSharp library. It may seem that this logic should be in the Application layer, and infrastructure objects should only implement files IO operations and converting html to pdf separately. But in theory with another implementation of the convertion, creation of the file may not be necessary.
Splitting the service at the Infrastructure level seems redundant, although it is easily implemented if needed.
