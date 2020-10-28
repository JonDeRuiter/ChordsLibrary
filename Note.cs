using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    //public enum NoteNames
    //{ //These seem to get snagged on a first 'instance of' basis. So I'm trying to put the more expected version first.
    //    C = 1,
    //    Bsharp = 1,
    //    Csharp = 2,
    //    Dflat = 2,
    //    D = 3,
    //    Eflat = 4,
    //    Dsharp = 4,
    //    E = 5,
    //    Fflat = 5,
    //    F = 6,
    //    Esharp = 6,
    //    Fsharp = 7,
    //    Gflat = 7,
    //    G = 8,
    //    Aflat = 9,
    //    Gsharp = 9,
    //    A = 10,
    //    Bflat = 11,
    //    Asharp = 11,
    //    B = 12,
    //    Cflat = 12        
    //}

    public class Note
    {
        //private NoteNames NoteNames { get; set; }
        private string PrimaryName { get; set; }
        private string SecondaryName { get; set; }
        private int index;
        private NoteList List { get; set; }

        public int Index { get { return index; } set { this.index = value; } }



        public Note(string primaryName, string secondaryName, int index)
        {
            this.PrimaryName = primaryName;
            this.SecondaryName = secondaryName;
            this.Index = index;
            this.List = new NoteList();
        }

        public Note(Note note)
        {
            this.PrimaryName = note.PrimaryName;
            this.SecondaryName = note.SecondaryName;
            this.Index = note.Index;           
        }

        public Note()
        {

        }

        public void MapNotes ()
        {
            //TODO: something here from UI to objects
        }

        public int FindNoteIncrement(Note root, Note toFind)
        {            
            int i = 0; //this is how we count beyond an octave

            //start counting at the root note
            Note checkNote = new Note(root);

            do
            {
                i++;
                this.List.Next(checkNote);
            } while (checkNote.index != toFind.index);

            return i;
        }
    }
}
