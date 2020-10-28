using ChordsLibrary.DataAccess;
using System;
using System.Collections.Generic;

namespace ChordsLibrary
{
    public class Chord
    {
        public string ChordName { get; set; }
        public Note RootNote { get; set; }
        public List<Note> NoteList { get; set; }
        public int[] NoteDifference { get; set; }
        public string Message { get; set; }


        public Chord()
        {

        }

        public Chord(string chordName, List<Note> notes)
        {
            ChordName = chordName;
            NoteList = notes;
        }

        public Chord(Note rootNote, int[] noteDifference, string chordName)
        {
            RootNote = rootNote;
            ChordName = chordName;
            NoteDifference = noteDifference;
        }

        //Insert Chord if new
        public string InsertChord(Chord chord)
        {
            if (chord.IsNewChord(chord))
            {
                //insert chord + all variations of it root c through b
                //TODO: Figure out how to get chords with the effective same root note to map the the correct one - IE C instead of B Sharp
                //now just check to make sure that works
                List<Chord> chords = GenAllChordsFromEntry(chord);
                ChordDTO.InsertNewChord(chords);
            }
            else
            {
                //Don't insert chord
                return "Chord already exists.";
            }

            return "Chord saved successfuly";
        }

        public static Chord FindChord(List<Note> notesEntered)
        {
            Chord unknownChord = new Chord();
            unknownChord.NoteList = notesEntered;
            unknownChord.RootNote = notesEntered[0];

            unknownChord.NoteDifference = FindNoteRelationship(notesEntered);

            if (unknownChord.IsNewChord(unknownChord))
            {
                unknownChord.Message = "Either this is not a formal chord, or we don't have it in our system yet.";
            }
            else
            {
                unknownChord = ChordDAO.FindAChord(unknownChord);
                unknownChord.Message = "We found your chord!";
            }

            return unknownChord;
        }

        private bool IsNewChord(Chord chord)
        {
            Chord emptyChord = ChordDAO.FindAChord(chord);

            //I just picked an element that will be there everytime
            if (emptyChord.ChordName == null)
            {
                return true;
            }

            return false;
        }

        //the mathematical relationship between the root note and the rest of the notes in the chord define their chord. This method calculates those relationships
        private static int[] FindNoteRelationship(List<Note> noteNames)
        {
            //Need to check this to make sure it functions as expected - [0] should always hold a value of '0'
            int[] noteRel = new int[(noteNames.Count - 1)];


            int rootNote = (int)noteNames[0];
            int[] enumArray = EnumToIntArray(noteNames);
            //TODO: first find the index of the root, so that chord inversions work
            //but do I even want to safeguard these? That will make it harder to return the notes themselves
            for (int i = 1; i < enumArray.Length; i++)
            {
                int safeNote = SafeGuardNotes(rootNote, enumArray[i]);
                noteRel[(i - 1)] = safeNote - rootNote;
            }

            return noteRel;
        }

        //The enumerable list only represents an octaves worth of notes
        //real chords may go beyond those limitations but we can still use simple math to understand how they work
        private static int[] EnumToIntArray(List<NoteNames> notesEntered)
        {
            int[] notesByNum = new int[notesEntered.Count - 1];
            for (int i = 0; i < notesByNum.Length; i++)
            {
                notesByNum[i] = (int)notesEntered[i];
            }
            return notesByNum;

        }

        //Not all chords start near C, and even the ones that do may extend beyond an octave higher 
        //so we need to make sure the number representation can accurately define the relationships between notes
        private static int SafeGuardNotes(int rootNote, int note)
        {
            if (note > rootNote)
            {
                return note;
            }
            else
            {
                return (note + 12);
            }
        }



        private List<Chord> GenAllChordsFromEntry(Chord origChord)
        {
            List<Chord> allRoots = new List<Chord>();

            for (int i = 1; i < 13; i++)
            {
                Chord chord = new Chord((NoteNames)i, origChord.NoteDifference, origChord.ChordName);
                allRoots.Add(chord);
            }

            return allRoots;
        }

        private string ParseChordName(string sbmtdName, int i)
        {
            string parsedName;
            int space = sbmtdName.IndexOf(" "); //First set of characters should be the root note
            parsedName = ((NoteNames)i).ToString() + sbmtdName.Substring(space);

            return parsedName;
        }


    }
}
