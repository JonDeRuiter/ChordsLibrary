using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    public class NoteList
    {

        private Note[] noteTree;

        public Note[] NoteTree { get { return noteTree; }  set { this.noteTree = value; } }
               
        public NoteList()
        {
            char sharp = '\x266f'; //UFT-16 encoding
            char flat = '\x266D';

            Note[] tmpNotes = new Note[12];

            tmpNotes[0] = new Note("A", null, 0);
            tmpNotes[1] = new Note("A" + sharp, "B" + flat, 1);
            tmpNotes[2] = new Note("B", "C" + flat, 2);
            tmpNotes[3] = new Note("C", "B" + sharp, 3);
            tmpNotes[4] = new Note("C" + sharp, "D" + flat, 4);
            tmpNotes[5] = new Note("D", null, 5);
            tmpNotes[6] = new Note("E" + flat, "D" + sharp, 6);
            tmpNotes[7] = new Note("E", null, 7);
            tmpNotes[8] = new Note("F", "E" + sharp, 8);
            tmpNotes[9] = new Note("F" + sharp, "G" + flat, 9);
            tmpNotes[10] = new Note("G", null, 10);
            tmpNotes[11] = new Note("G" + sharp, "A" + flat, 11);

            this.NoteTree = tmpNotes;
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

      public List<string> NotesToUI()
      {
            List<string> UINotes = new List<string>();

            NoteList list = new NoteList();

            foreach (Note note in list.NoteTree)
            {
                UINotes.Add(note.PrimaryName);
                if (!(note.SecondaryName == null))
                {
                    UINotes.Add(note.SecondaryName);
                }
            }

            return UINotes;
      }
    }
}
