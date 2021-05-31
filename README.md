To run the solution, do the following:
<ul>
  <li>First, execute an update-database command in Package Manager Console, make sure that the default startup project is set to Infrastructure/Repositories - This creates the database, and apply all migrations.</li>
  <li>Set the startup project to WebApi.MarvelConvention, and run the solution. - This step start the web api on https://localhost:44376/</li>
  <li>
    Start the client by either using GitBash or CMD, and navigate to the client project - then execute ng serve command. This should start the client on http://localhost:4200  
  </li>
 </ul>
</br>
Requirements

<ul>
  <li>
    As an admin I want to create a conventions with or without an speaker, so that participants can reserve or participate in a convention. If an convention don’t have a Speaker, it will be closed for participant.
  </li>
  <li>
    As an admin I want to set the max number of seats at a convention. The convention should be closed, when all the seats are taken.
  </li>
  <li>
    As an participant I can choose to participate in, or reserve a seat to a convention.
  </li>
  <li>
    As an participant I want an overview of my conventions. Both what I have reserved or participate in.
  </li>
  <li>
    As a speaker I want an overview of conventions, that does not have an speaker assigned.
  </li>
  <li>
    As a speaker I want to add myself to an convention that does not have an speaker assigned.
  </li>
  <li>
    As a speaker I want an overview of my conventions, participate as a speaker.
  </li>
 </ul>
 
 <p>Onion architecture</p>
 <img src="https://user-images.githubusercontent.com/13518751/120244366-4bdc9000-c26a-11eb-8edf-6da06e098c7f.png">
<p>
Dependency diagram
</p>
<img src="https://user-images.githubusercontent.com/13518751/120244483-9958fd00-c26a-11eb-891f-36caa56bcef1.png">
<p>
  Sequence diagram
</p>
<img src="https://user-images.githubusercontent.com/13518751/120244546-c7d6d800-c26a-11eb-871b-34c1106dc65a.png">




