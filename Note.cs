﻿using System;
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
        private string primaryName { get; set; }
        private string SecondaryName { get; set; }
        private int index;
        private NoteList List { get; set; }

        public string PrimaryName { get { return primaryName; } set { this.primaryName = value; } }
        public int Index { get { return index; } set { this.index = value; } }



        public Note(string primaryName, string secondaryName, int index)
        {
            this.primaryName = primaryName;
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

        public static List<Note> MapNotes (string[] notesInChord)
        {
            //TODO: More options for mapping as overlaods
            List<Note> chordNotes = MapNoteNames(notesInChord);

            return chordNotes;

        }

        private List<Note> MapNoteNames(string[] notesInChord)
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

            do
            {
                i++;
                root.List.Next(checkNote);
            } while (checkNote.index != toFind.index);

            return i;
        }
    }
}
