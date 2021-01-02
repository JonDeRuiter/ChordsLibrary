using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{

    public class Note
    {
        //private NoteNames NoteNames { get; set; }
        private string primaryName { get; set; }
        private string secondaryName { get; set; }
        private int index;
        //private NoteList List { get; set; }

        public string PrimaryName { get { return primaryName; } set { this.primaryName = value; } }
        public string SecondaryName { get { return secondaryName; } set { this.secondaryName = value; } }
        public int Index { get { return index; } set { this.index = value; } }

        //NoteList List = new NoteList();

        public Note(string primaryName, string secondaryName, int index)
        {
            this.primaryName = primaryName;
            this.secondaryName = secondaryName;
            this.Index = index;
            //this.List = new NoteList(); 
        }

        public Note(Note note)
        {
            this.primaryName = note.PrimaryName;
            this.secondaryName = note.SecondaryName;
            this.Index = note.Index;           
        }

        public Note()
        {

        }

        public static List<Note> MapNotes (List<string> notesInChord)
        {
            //TODO: More options for mapping as overlaods            
            List<Note> chordNotes = MapNoteNames(notesInChord);

            return chordNotes;

        }

        private static List<Note> MapNoteNames(List<string> notesInChord)
        {
            NoteList oNotes = new NoteList();
            List<Note> mappedNotes = new List<Note>();
            char sharp = '\x266f'; //UFT-16 encoding
            char flat = '\x266D';

            foreach (string note in notesInChord)
            {
                if (note == "B" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[3]);
                }
                else if (note == "C")
                {
                    mappedNotes.Add(oNotes.NoteTree[3]);
                }
                else if (note == "C" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[4]);
                }
                else if (note == "D" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[4]);
                }
                else if (note == "D")
                {
                    mappedNotes.Add(oNotes.NoteTree[5]);
                }
                else if (note == "D" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[6]);
                }
                else if (note == "E" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[6]);
                }
                else if (note == "E")
                {
                    mappedNotes.Add(oNotes.NoteTree[7]);
                }
                else if (note == "F" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[7]);
                }
                else if (note == "E" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[8]);
                }                
                else if (note == "F")
                {
                    mappedNotes.Add(oNotes.NoteTree[8]);
                }
                else if (note == "F" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[9]);
                }
                else if (note == "G" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[9]);
                }
                else if (note == "G")
                {
                    mappedNotes.Add(oNotes.NoteTree[10]);
                }
                else if (note == "G" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[11]);
                }
                else if (note == "A" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[11]);
                }
                else if (note == "A")
                {
                    mappedNotes.Add(oNotes.NoteTree[0]);
                }
                else if (note == "A" + sharp)
                {
                    mappedNotes.Add(oNotes.NoteTree[1]);
                }
                else if (note == "B" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[1]);
                }
                else if (note == "B")
                {
                    mappedNotes.Add(oNotes.NoteTree[2]);
                }
                else if (note == "C" + flat)
                {
                    mappedNotes.Add(oNotes.NoteTree[2]);
                }
                else
                {
                    throw new Exception("How did you... this isn't even a note!!!");
                }
            }
            return mappedNotes;
        }


        public static int FindNoteIncrement(Note root, Note toFind)
        {            
            int i = 0; //this is how we count beyond an octave

            //start counting at the root note
            Note checkNote = new Note(root);
            NoteList list = new NoteList();

            while (checkNote.index != toFind.index)
            {
                i++;                
                checkNote = list.Next(checkNote);
            } 

            return i;
        }
    }
}
