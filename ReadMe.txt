I've created this library to handle the basic functions of finding the names of chords. I hope to use it as a base for a couple other projects and possibly extend it.

To add this to another project, open that project in MVS, right click on the project in solution explorer and add it as a resource.

Exposed methods:
	InsertChord
	FindChord
	GetAllChords -- intended for diagnostics of chord data


A couple TODOs in the code that aren't necessary to start testing it.
Here are some on my list a little further out:
1. Make the files 'hidden'
2. Assess what refactoring would be helpful
3. Do I really need header and footer properties for the DTO and DAO? I don't think I use them