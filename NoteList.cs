using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    class NoteList
    {

        public Note[] NoteTree { get { return NoteTree; } set { this.NoteTree = value; } }
               
        public NoteList()
        {
            this.NoteTree[0] = new Note("A", null, 0);
            this.NoteTree[1] = new Note("Asharp", "Bflat", 1);
            this.NoteTree[2] = new Note("B", "Cflat", 2);
            this.NoteTree[3] = new Note("C", "Bsharp", 3);
            this.NoteTree[4] = new Note("Csharp", "Dflat", 4);
            this.NoteTree[5] = new Note("D", null, 5);
            this.NoteTree[6] = new Note("Eflat", "Dsharp", 6);
            this.NoteTree[7] = new Note("E", null, 7);
            this.NoteTree[8] = new Note("F", "Esharp", 8);
            this.NoteTree[9] = new Note("Fsharp", "Gflat", 9);
            this.NoteTree[10] = new Note("G", null, 10);
            this.NoteTree[11] = new Note("Gsharp", "Aflat", 11);
        }

        public Note Next(Note note)
        {
            //NoteList list = new NoteList();
            Note nextNote = new Note();
            
            if (note.Index != 11) //if we haven't bridged an octave
            {
                nextNote = this.NoteTree[note.Index + 1];
            }
            else //if we have bridged an octave then start at A
            {
                nextNote = this.NoteTree[0];
            }

            return nextNote;
        }

      
    }
}
